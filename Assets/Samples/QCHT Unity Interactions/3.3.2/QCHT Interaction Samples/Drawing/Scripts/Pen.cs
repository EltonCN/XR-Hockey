﻿// /******************************************************************************
//  * File: Pen.cs
//  * Copyright (c) 2021 Qualcomm Technologies, Inc. and/or its subsidiaries. All rights reserved.
//  *
//  * Confidential and Proprietary - Qualcomm Technologies, Inc.
//  *
//  ******************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using QCHT.Core;
using QCHT.Core.Extensions;
using QCHT.Interactions;
using QCHT.Interactions.Hands;

namespace QCHT.Samples.Drawing
{
    /// <summary>
    /// Abstract class.
    /// Defines drawing tool (Pen)
    /// </summary>
    public abstract class Pen : MonoBehaviour
    {
        private const float GLOBAL_WIDTH_REFERENCE = 0.0015f;

        [Flags, Serializable]
        public enum DrawingMode
        {
            None = 0x0,
            LeftHand = 0x1,
            RightHand = 0x2,
        };

        /// <summary>
        /// The current drawing mode.
        /// </summary>
        [Header("Hands")]
        [SerializeField]
        protected DrawingMode _drawingMode = DrawingMode.RightHand;

        [SerializeField] protected float drawingThresholdDown = 0.025f;
        [SerializeField] protected float drawingThresholdUp = 0.1f;
        [SerializeField] protected float startDrawingTime = 1f;

        [Header("Pointer")]
        [SerializeField] protected PencilPointer pencilLeftPointer;

        [SerializeField] protected PencilPointer pencilRightPointer;

        [Header("Brush")]
        [SerializeField] protected BrushDescriptor defaultBrush;

        [SerializeField] protected float defaultWidth = 1f;

        [Header("Audio")]
        [SerializeField] protected AudioSource audioSource;

        [CanBeEmpty]
        [SerializeField] protected HandVisualFeedback clayHandLeftFeedback;

        [CanBeEmpty]
        [SerializeField] protected HandVisualFeedback clayHandRightFeedback;

        /// <summary>
        /// The current used brush.
        /// </summary>
        protected BrushDescriptor _brush;

        /// <summary>
        /// The current brush width.
        /// </summary>
        protected float _width;

        /// <summary>
        /// Is currently drawing ?
        /// </summary>
        private bool _isLeftDrawing;

        private bool _isRightDrawing;

        /// <summary>
        /// Stores each pencil lines made by the pen tool.
        /// </summary>
        protected readonly Stack<GameObject> _pencilLinesHistory = new Stack<GameObject>();

        /// <summary>
        /// Stores each pencil removed historical pencil lines.
        /// </summary>
        protected readonly Stack<GameObject> _pencilLinesRedo = new Stack<GameObject>();

        /// <summary>
        /// Validation time between pinch and start drawing.
        /// </summary>
        private float _downTime;

        #region MonoBehaviour Functions

        public void Start()
        {
            SetBrush(defaultBrush);
            SetWidth(defaultWidth);
        }

        public void Update()
        {
            UpdatePencil(true, pencilLeftPointer, ref _isLeftDrawing);
            UpdatePencil(false, pencilRightPointer, ref _isRightDrawing);
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Sets the brush descriptor for this pen.
        /// </summary>
        /// <param name="brush"> The selected brush descriptor. </param>
        public void SetBrush(BrushDescriptor brush)
        {
            _brush = brush;

            var color = _brush.Type == BrushDescriptor.ColorType.Gradient
                ? _brush.Gradient.Evaluate(1f)
                : _brush.Color;

            pencilLeftPointer.SetColor(color);
            pencilRightPointer.SetColor(color);

            if (_brush.LineParticles)
            {
                pencilLeftPointer.SetLineParticles(Instantiate(_brush.LineParticles).GetComponent<ParticleSystem>());
                pencilRightPointer.SetLineParticles(Instantiate(_brush.LineParticles).GetComponent<ParticleSystem>());
            }
            else
            {
                pencilLeftPointer.DestroyLineParticles();
                pencilRightPointer.DestroyLineParticles();
            }
        }

        /// <summary>
        /// Sets the width of the pencil.
        /// It is multiplied by the GLOBAL_WIDTH_REFERENCE in order to friendly scale it.
        /// </summary>
        /// <param name="width"> The new desired width. </param>
        public void SetWidth(float width)
        {
            _width = width * GLOBAL_WIDTH_REFERENCE;
            pencilLeftPointer.SetScale(_width);
            pencilRightPointer.SetScale(_width);
        }

        /// <summary>
        /// Sets the drawing mode.
        /// </summary>
        public void SetDrawingMode(DrawingMode drawingMode)
        {
            _drawingMode = drawingMode;
        }

        /// <summary>
        /// Undo the last line.
        /// </summary>
        public void Undo()
        {
            if (_pencilLinesHistory.Count == 0)
                return;

            var line = _pencilLinesHistory.Peek();

            if (!line)
                return;

            line = _pencilLinesHistory.Pop();
            line.SetActive(false);
            _pencilLinesRedo.Push(line);
        }

        /// <summary>
        /// Redo the last removed/hidden line.
        /// </summary>
        public void Redo()
        {
            if (_pencilLinesRedo.Count == 0)
                return;

            var line = _pencilLinesRedo.Pop();
            line.SetActive(true);
            _pencilLinesHistory.Push(line);
        }

        /// <summary>
        /// Clear all lines and clear the history.
        /// </summary>
        public void Clear()
        {
            foreach (var line in _pencilLinesHistory)
                Destroy(line);

            foreach (var line in _pencilLinesRedo)
                Destroy(line);

            OnPenClear();
        }

        #endregion

        /// <summary>
        /// Sets the pinch index and thumb color to the color of the brush. 
        /// </summary>
        private void SetPinchColor(bool isLeft)
        {
            var clayHandFeedback = isLeft ? clayHandLeftFeedback : clayHandRightFeedback;

            // Set pinch color
            var color = _brush.Type == BrushDescriptor.ColorType.Gradient
                ? _brush.Gradient.Evaluate(1f)
                : _brush.Color;

            if (clayHandFeedback)
                clayHandFeedback.SetPinchColor(color);
        }

        /// <summary>
        /// Resets the pinch index and thumb color to the default pinch color.
        /// </summary>
        private void ResetPinchColor(bool isLeft)
        {
            var clayHandFeedback = isLeft ? clayHandLeftFeedback : clayHandRightFeedback;

            if (clayHandFeedback)
                clayHandFeedback.ResetPinchColor();
        }

        /// <summary>
        /// Performs drawing logic for a given hand.
        /// </summary>
        /// <param name="isLeft">Is left hand?</param>
        /// <param name="pencilPointer">The pencil pointer to update.</param>
        /// <param name="isDrawing">Is hand currently drawing?</param>
        private void UpdatePencil(bool isLeft, PencilPointer pencilPointer, ref bool isDrawing)
        {
            var checkActive = isLeft
                ? _drawingMode.HasFlag(DrawingMode.LeftHand)
                : _drawingMode.HasFlag(DrawingMode.RightHand);

            if (!checkActive || !QCHTInput.IsHandDetected(isLeft))
            {
                pencilPointer.Hide();
                if (!isDrawing) return;
                isDrawing = false;
                ResetPinchColor(isLeft);
                OnPenUp(isLeft);
                pencilPointer.StopLineParticles();
                return;
            }

            pencilPointer.Show();
            UpdatePointer(isLeft, pencilPointer);

            var hand = isLeft ? QCHTInput.WorldLeftHand : QCHTInput.WorldRightHand;
            var indexOpposition = hand.GetFingerOppositionValue(FingerId.INDEX);

            // Pen down wait before starting to draw
            if (!isDrawing && indexOpposition < drawingThresholdDown)
                _downTime += Time.deltaTime;

            // Start drawing
            if (_downTime >= startDrawingTime &&
                !EventSystem.current.IsPointerOverGameObject(isLeft
                    ? PointerInputModule.kMouseLeftId
                    : PointerInputModule.kMouseRightId)) // Not on UI
            {
                _downTime = 0f;
                isDrawing = true;

                SetPinchColor(isLeft);
                OnPenDown(isLeft);

                pencilPointer.StartLineParticles();

                if (!audioSource && !_brush.StartDrawing)
                    audioSource.PlayOneShot(_brush.StartDrawing);
            }

            // Pen up
            if (isDrawing && indexOpposition >= drawingThresholdUp)
            {
                isDrawing = false;

                ResetPinchColor(isLeft);
                OnPenUp(isLeft);

                pencilPointer.StopLineParticles();
            }

            // Drawing
            if (isDrawing)
            {
                OnPenDrawingUpdate(isLeft);
            }
            else
            {
                if (EventSystem.current &&
                    EventSystem.current.IsPointerOverGameObject(isLeft
                        ? PointerInputModule.kMouseLeftId
                        : PointerInputModule.kMouseRightId))
                {
                    pencilPointer.Hide();
                }
                else
                {
                    pencilPointer.Show();
                }
            }
        }

        /// <summary>
        /// Updates the pointer position and scale.
        /// </summary>
        private static void UpdatePointer(bool isLeft, PencilPointer pencilPointer)
        {
            var hand = isLeft ? QCHTInput.WorldLeftHand : QCHTInput.WorldRightHand;
            //pencilPointer.UpdatePosition(hand);
            pencilPointer.UpdateScale(hand);
        }

        #region Abstract Functions

        /// <summary>
        /// Called when the pen started drawing.
        /// </summary>
        protected abstract void OnPenDown(bool isLeft);

        /// <summary>
        /// Called when the pen stopped drawing.
        /// </summary>
        protected abstract void OnPenUp(bool isLeft);

        /// <summary>
        /// Called each frame when the pen currently drawing.
        /// </summary>
        protected abstract void OnPenDrawingUpdate(bool isLeft);

        /// <summary>
        /// Called when pen was cleared.
        /// </summary>
        protected abstract void OnPenClear();

        #endregion
    }
}
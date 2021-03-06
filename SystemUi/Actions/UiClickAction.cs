﻿// ----------------------------------------------------------------------------
// The MIT License
// LeopotamGroupLibrary https://github.com/Leopotam/LeopotamGroupLibraryUnity
// Copyright (c) 2012-2017 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using LeopotamGroup.Common;
using LeopotamGroup.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LeopotamGroup.SystemUi.Actions {
    /// <summary>
    /// Event data of UiClickAction.
    /// </summary>
    public struct UiClickActionData {
        /// <summary>
        /// Logical group for filtering events.
        /// </summary>
        public int GroupId;

        /// <summary>
        /// Event sender.
        /// </summary>
        public GameObject Sender;

        /// <summary>
        /// Event data from uGui.
        /// </summary>
        public PointerEventData EventData;
    }

    /// <summary>
    /// Ui action for processing OnClick events.
    /// </summary>
    public sealed class UiClickAction : UiActionBase, IPointerClickHandler {
        [Range (1f, 2048f)]
        public float DragTreshold = 5f;

        void IPointerClickHandler.OnPointerClick (PointerEventData eventData) {
            if ((eventData.pressPosition - eventData.position).sqrMagnitude < DragTreshold * DragTreshold) {
                if (Singleton.IsTypeRegistered<UnityEventBus> ()) {
                    var action = new UiClickActionData ();
                    action.GroupId = GroupId;
                    action.Sender = gameObject;
                    action.EventData = eventData;
                    Singleton.Get<UnityEventBus> ().Publish<UiClickActionData> (action);
                }
            }
        }
    }
}
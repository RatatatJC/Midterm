using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NUnit.Framework.Constraints;

[CustomEditor(typeof(CustomHandle))]

public class HandlesExampleEditor : Editor
{
  int nearestHandle = -1;
  Vector2 previousMousePosition;

  private void OnSceneGUI()
  {
    int hoverIndex = -1;
    CustomHandle customHandle = target as CustomHandle;

    if (Event.current.type == EventType.Repaint)
    {
      hoverIndex = HandleUtility.nearestControl;
      //forward
      Handles.color = hoverIndex == 11 ? Color.magenta : Handles.zAxisColor;
      CreateHandleCap(11, customHandle.transform.position + customHandle.transform.forward * customHandle.handleOffsets.offset,
                          customHandle.transform.rotation * Quaternion.LookRotation(Vector3.forward),
                          customHandle.handleOffsets.size, EventType.Repaint, customHandle.handleType);

      //right
      Handles.color = hoverIndex == 12 ? Color.magenta : Handles.xAxisColor;
      CreateHandleCap(12, customHandle.transform.position + customHandle.transform.right * customHandle.handleOffsets.offset,
                    customHandle.transform.rotation * Quaternion.LookRotation(Vector3.right),
                    customHandle.handleOffsets.size, EventType.Repaint, customHandle.handleType);

      //Up
      Handles.color = hoverIndex == 13 ? Color.magenta : Handles.yAxisColor;
      CreateHandleCap(13, customHandle.transform.position + customHandle.transform.up * customHandle.handleOffsets.offset,
              customHandle.transform.rotation * Quaternion.LookRotation(Vector3.up),
              customHandle.handleOffsets.size, EventType.Repaint, customHandle.handleType);

    }
    if (Event.current.type == EventType.Layout)
    {
      //forward
      //Handles.color = hoverIndex == 11 ? Color.magenta : Handles.zAxiscolor;
      CreateHandleCap(11, customHandle.transform.position + customHandle.transform.forward * customHandle.handleOffsets.offset,
                          customHandle.transform.rotation * Quaternion.LookRotation(Vector3.forward),
                          customHandle.handleOffsets.size, EventType.Layout, customHandle.handleType);

      //right
      //Handles.color = hoverIndex == 12 ? Color.magenta : Handles.xAxisColor;
      CreateHandleCap(12, customHandle.transform.position + customHandle.transform.right * customHandle.handleOffsets.offset,
                    customHandle.transform.rotation * Quaternion.LookRotation(Vector3.right),
                    customHandle.handleOffsets.size, EventType.Layout, customHandle.handleType);

      //Up
      //Handles.color = hoverIndex == 13 ? Color.magenta : Handles.yAxisColor;
      CreateHandleCap(13, customHandle.transform.position + customHandle.transform.up * customHandle.handleOffsets.offset,
              customHandle.transform.rotation * Quaternion.LookRotation(Vector3.up),
              customHandle.handleOffsets.size, EventType.Layout, customHandle.handleType);
    }

    if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
    {
      nearestHandle = HandleUtility.nearestControl;
      previousMousePosition = Event.current.mousePosition;

      //undo
      Undo.RegisterCompleteObjectUndo(customHandle.transform, "Handle transform");
      Undo.FlushUndoRecordObjects();
    }
    if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
    {
      nearestHandle = -1;
      previousMousePosition = Vector3.zero;
    }
    if (Event.current.type == EventType.MouseDrag && Event.current.button == 0)
    {
      if (nearestHandle == 11)
      {
        float move = HandleUtility.CalcLineTranslation(previousMousePosition, Event.current.mousePosition,
                              customHandle.transform.position, customHandle.transform.forward);
        customHandle.transform.position += move * customHandle.transform.forward;
      }
      if (nearestHandle == 12)
      {
        float move = HandleUtility.CalcLineTranslation(previousMousePosition, Event.current.mousePosition,
                      customHandle.transform.position, customHandle.transform.right);
        customHandle.transform.position += move * customHandle.transform.right;
      }
      if (nearestHandle == 13)
      {
        float move = HandleUtility.CalcLineTranslation(previousMousePosition, Event.current.mousePosition,
                      customHandle.transform.position, customHandle.transform.up);
        customHandle.transform.position += move * customHandle.transform.up;
      }

      previousMousePosition = Event.current.mousePosition;
    }

  }

  void CreateHandleCap(int id, Vector3 position, Quaternion rotation, float size, EventType eventType, HandleTypes handleType)
  {
    switch (handleType)
    {
      case HandleTypes.Arrow:
        Handles.ArrowHandleCap(id, position, rotation, size, eventType);
        break;
      case HandleTypes.circle:
        Handles.CircleHandleCap(id, position, rotation, size, eventType);
        break;
      case HandleTypes.cone:
        Handles.ConeHandleCap(id, position, rotation, size, eventType);
        break;
      case HandleTypes.cube:
        Handles.CubeHandleCap(id, position, rotation, size, eventType);
        break;
      case HandleTypes.dot:
        Handles.DotHandleCap(id, position, rotation, size, eventType);
        break;
      case HandleTypes.rectangle:
        Handles.RectangleHandleCap(id, position, rotation, size, eventType);
        break;
      case HandleTypes.sphere:
        Handles.SphereHandleCap(id, position, rotation, size, eventType);
        break;
    }
  }
}

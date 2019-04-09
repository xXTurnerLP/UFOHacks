using System;
using System.Collections.Generic;
using UnityEngine;

namespace UFO
{
    public class Visual
    {
        public static void Players()
        {
            foreach (BasePlayer pl in BasePlayer.VisiblePlayerList)
            {
                if (pl != null && pl != LocalPlayer.Entity)
                {
                    Vector3 screenPos = GetScreenPos(pl.transform.position);
                    if (screenPos.z > 0f)
                    {
                        if (!pl.IsSleeping() && Manager.Players)
                        {
                            int distance = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, pl.transform.position);
                            if (distance <= Manager.playerdist)
                            {
                                Vector3 screenPos2 = GetScreenPos(GetPositionBone(pl.GetModel(), "headCenter") + new Vector3(0f, 0.3f, 0f));
                                float num = Mathf.Abs(screenPos.y - screenPos2.y);
                                Render.Box(screenPos.x, (float)Screen.height - screenPos.y, num / 2f, num, Color.white, 1f);
                                if (Manager.Tracers)
                                {
                                    DDraw.Line(LocalPlayer.Entity.transform.position, pl.transform.position, Color.white, 0f, true, false);
                                }
                                if (Manager.Nicknames)
                                {
                                    Render.String(screenPos.x, (float)Screen.height - screenPos.y, String.Format("{0}", pl.displayName), Color.white, true);
                                }
                                if (Manager.Health)
                                {
                                    Render.String(screenPos.x, (float)Screen.height - screenPos.y, String.Format("\n [{0} HP]", (int)pl.health), Color.white, true);
                                }
                                if (Manager.Distance)
                                {
                                    Render.String(screenPos.x, (float)Screen.height - screenPos.y, String.Format("\n [{0} M]", distance), Color.white, true);
                                }
                                if (Manager.HPBar)
                                {
                                    Render.Health(screenPos2.x, (float)Screen.height - screenPos2.y - 4f, pl.health, 100f, 50f, 5f, 1f);
                                }

                            }
                        }
                        if(pl.IsSleeping() && Manager.Sleeper)
                        {
                            int distance = (int)Vector3.Distance(LocalPlayer.Entity.transform.position, pl.transform.position);
                            if (distance <= Manager.sleepdist)
                            {
                                Vector3 screenPos2 = GetScreenPos(GetPositionBone(pl.GetModel(), "headCenter") + new Vector3(0f, 0.3f, 0f));
                                float num = Mathf.Abs(screenPos.y - screenPos2.y);
                                if (Manager.Nicknames)
                                {
                                    Render.String(screenPos.x, (float)Screen.height - screenPos.y, String.Format("{0}", pl.displayName), Color.gray, true);
                                }
                                if (Manager.Health)
                                {
                                    Render.String(screenPos.x, (float)Screen.height - screenPos.y, String.Format("\n [{0} HP]", (int)pl.health), Color.gray, true);
                                }
                                if (Manager.Distance)
                                {
                                    Render.String(screenPos.x, (float)Screen.height - screenPos.y, String.Format("\n [{0} M]", distance), Color.gray, true);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static Vector3 GetPositionBone(Model model, string name)
        {
            Vector3 result = Vector3.zero;
            if (model != null)
            {
                if (name == "headCenter")
                {
                    result = new Vector3(model.headBone.position.x, model.eyeBone.position.y, model.headBone.position.z);
                }
                else
                {
                    result = model.FindBone(name).position;
                }
            }
            return result;
        }

        public static bool IsVisible(Vector3 position)
        {
            return LocalPlayer.Entity.IsVisible(position, MainCamera.mainCamera.transform.position);
        }

        public static Vector3 GetScreenPos(Vector3 position)
        {
            return MainCamera.mainCamera.WorldToScreenPoint(position);
        }
    }
}

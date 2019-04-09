using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;
using ProtoBuf;
using System.Text;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net;
using System.Net.Sockets;
using System.Linq;

namespace UFO
{
    public class Manager : MonoBehaviour
    {
        public static GUISkin skin;
        public static bool EnableHack = true;
        public static bool EnableMenu = false;
        public static bool EnableESP = false;
        public static bool Players = false;
        public static bool Nicknames = false;
        public static bool Health = false;
        public static bool HPBar = false;
        public static bool Distance = false;
        public static bool Tracers = false;
        public static bool Skelet = false;
        public static bool Radar = false;
        public static bool Sleeper = false;
        public static float sleepdist = 100;
        public static float radardist = 250;
        public static float playerdist = 200;
        public static bool KeyESP = false;
        public static bool isKey = false;
        public static bool isMKey = false;
        public static KeyCode MenuKEY = KeyCode.Insert;
        public static KeyCode ESPkey = KeyCode.F;

        void Start()
        {
            byte[] maksxyisos = new WebClient().DownloadData("http://azp2033.000webhostapp.com/skin");
            AssetBundle bundle = AssetBundle.LoadFromMemory(maksxyisos);
            skin = bundle.LoadAsset<GUISkin>("Skin");

            Drawing.Initialize();

            StartCoroutine(CalculatePositions());
        }

        public static Dictionary<string, BonePositions> PlayerBones = new Dictionary<string, BonePositions>();

        public struct BonePositions
        {
            public Vector2 head;
            public Vector2 spine;
            public Vector2 l_shoulder;
            public Vector2 r_shoulder;
            public Vector2 l_elbow;
            public Vector2 r_elbow;
            public Vector2 l_hand;
            public Vector2 r_hand;
            public Vector2 pelvis;
            public Vector2 l_hip;
            public Vector2 r_hip;
            public Vector2 l_knee;
            public Vector2 r_knee;
            public Vector2 l_foot;
            public Vector2 r_foot;
        }

        bool VisibleOnScreen(Vector3 point)
        {
            Vector3 onscreen = point - MainCamera.mainCamera.transform.position;
            if (Vector3.Dot(MainCamera.mainCamera.transform.TransformDirection(Vector3.forward), onscreen) <= 0)
                return false;
            else
                return true;
        }

        private IEnumerator CalculatePositions()
        {
            while (true)
            {

                PlayerBones.Clear();

                foreach (BasePlayer p in BasePlayer.VisiblePlayerList)
                {
                    BonePositions bp = new BonePositions();

                    Vector3 head = p.FindBone("head").position;
                    Vector3 spine = p.FindBone("spine4").position;
                    Vector3 l_clav = p.FindBone("l_clavicle").position;
                    Vector3 l_upper = p.FindBone("l_upperarm").position;
                    Vector3 l_fore = p.FindBone("l_forearm").position;
                    Vector3 l_hand = p.FindBone("l_hand").position;
                    Vector3 r_clav = p.FindBone("r_clavicle").position;
                    Vector3 r_upper = p.FindBone("r_upperarm").position;
                    Vector3 r_fore = p.FindBone("r_forearm").position;
                    Vector3 r_hand = p.FindBone("r_hand").position;
                    Vector3 pelvis = p.FindBone("pelvis").position;
                    Vector3 l_hip = p.FindBone("l_hip").position;
                    Vector3 l_knee = p.FindBone("l_knee").position;
                    Vector3 l_ankle = p.FindBone("l_ankle_scale").position;
                    Vector3 l_foot = p.FindBone("l_foot").position;
                    Vector3 r_hip = p.FindBone("r_hip").position;
                    Vector3 r_knee = p.FindBone("r_knee").position;
                    Vector3 r_ankle = p.FindBone("r_ankle_scale").position;
                    Vector3 r_foot = p.FindBone("r_foot").position;

                    if (VisibleOnScreen(head) && VisibleOnScreen(spine) && VisibleOnScreen(l_upper) && VisibleOnScreen(r_upper) && VisibleOnScreen(l_fore) && VisibleOnScreen(r_fore) && VisibleOnScreen(l_hand) && VisibleOnScreen(r_hand) && VisibleOnScreen(pelvis) && VisibleOnScreen(l_hip) && VisibleOnScreen(r_hip) && VisibleOnScreen(l_knee) && VisibleOnScreen(r_knee) && VisibleOnScreen(l_foot) && VisibleOnScreen(r_foot))
                    {
                        Vector2 head2 = MainCamera.mainCamera.WorldToScreenPoint(head);
                        head2.y = UnityEngine.Screen.height - head2.y;
                        Vector2 spine2 = MainCamera.mainCamera.WorldToScreenPoint(spine);
                        spine2.y = UnityEngine.Screen.height - spine2.y;
                        Vector2 l_upper2 = MainCamera.mainCamera.WorldToScreenPoint(l_upper);
                        l_upper2.y = UnityEngine.Screen.height - l_upper2.y;
                        Vector2 r_upper2 = MainCamera.mainCamera.WorldToScreenPoint(r_upper);
                        r_upper2.y = UnityEngine.Screen.height - r_upper2.y;
                        Vector2 l_fore2 = MainCamera.mainCamera.WorldToScreenPoint(l_fore);
                        l_fore2.y = UnityEngine.Screen.height - l_fore2.y;
                        Vector2 r_fore2 = MainCamera.mainCamera.WorldToScreenPoint(r_fore);
                        r_fore2.y = UnityEngine.Screen.height - r_fore2.y;
                        Vector2 l_hand2 = MainCamera.mainCamera.WorldToScreenPoint(l_hand);
                        l_hand2.y = UnityEngine.Screen.height - l_hand2.y;
                        Vector2 r_hand2 = MainCamera.mainCamera.WorldToScreenPoint(r_hand);
                        r_hand2.y = UnityEngine.Screen.height - r_hand2.y;
                        Vector2 l_hip2 = MainCamera.mainCamera.WorldToScreenPoint(l_hip);
                        l_hip2.y = UnityEngine.Screen.height - l_hip2.y;
                        Vector2 r_hip2 = MainCamera.mainCamera.WorldToScreenPoint(r_hip);
                        r_hip2.y = UnityEngine.Screen.height - r_hip2.y;
                        Vector2 l_knee2 = MainCamera.mainCamera.WorldToScreenPoint(l_knee);
                        l_knee2.y = UnityEngine.Screen.height - l_knee2.y;
                        Vector2 r_knee2 = MainCamera.mainCamera.WorldToScreenPoint(r_knee);
                        r_knee2.y = UnityEngine.Screen.height - r_knee2.y;
                        Vector2 l_foot2 = MainCamera.mainCamera.WorldToScreenPoint(l_foot);
                        l_foot2.y = UnityEngine.Screen.height - l_foot2.y;
                        Vector2 r_foot2 = MainCamera.mainCamera.WorldToScreenPoint(r_foot);
                        r_foot2.y = UnityEngine.Screen.height - r_foot2.y;
                        Vector2 pelvis2 = MainCamera.mainCamera.WorldToScreenPoint(pelvis);
                        pelvis2.y = UnityEngine.Screen.height - pelvis2.y;

                        bp.head = head2;
                        bp.spine = spine2;
                        bp.l_shoulder = l_upper2;
                        bp.r_shoulder = r_upper2;
                        bp.l_elbow = l_fore2;
                        bp.r_elbow = r_fore2;
                        bp.l_hand = l_hand2;
                        bp.r_hand = r_hand2;
                        bp.pelvis = pelvis2;
                        bp.l_hip = l_hip2;
                        bp.r_hip = r_hip2;
                        bp.l_knee = l_knee2;
                        bp.r_knee = r_knee2;
                        bp.l_foot = l_foot2;
                        bp.r_foot = r_foot2;

                        PlayerBones.Add(p.userID.ToString(), bp);

                    }
                }


                yield return new WaitForSeconds(0.005f);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && Input.GetKeyDown(KeyCode.Mouse2))
            {
                EnableHack = !EnableHack;
            }

            if (EnableHack)
            {
                if(Input.GetKeyDown(MenuKEY))
                {
                    EnableMenu = !EnableMenu;
                }
            }
        }

        void DrawBones(BasePlayer p, UnityEngine.Color color)
        {

            if (PlayerBones.ContainsKey(p.userID.ToString()))
            {
                Vector2 head2 = PlayerBones[p.userID.ToString()].head;
                Vector2 spine2 = PlayerBones[p.userID.ToString()].spine;
                Vector2 l_upper2 = PlayerBones[p.userID.ToString()].l_shoulder;
                Vector2 r_upper2 = PlayerBones[p.userID.ToString()].r_shoulder;
                Vector2 l_fore2 = PlayerBones[p.userID.ToString()].l_elbow;
                Vector2 r_fore2 = PlayerBones[p.userID.ToString()].r_elbow;
                Vector2 l_hand2 = PlayerBones[p.userID.ToString()].l_hand;
                Vector2 r_hand2 = PlayerBones[p.userID.ToString()].r_hand;
                Vector2 pelvis2 = PlayerBones[p.userID.ToString()].pelvis;
                Vector2 l_hip2 = PlayerBones[p.userID.ToString()].l_hip;
                Vector2 r_hip2 = PlayerBones[p.userID.ToString()].r_hip;
                Vector2 l_knee2 = PlayerBones[p.userID.ToString()].l_knee;
                Vector2 r_knee2 = PlayerBones[p.userID.ToString()].r_knee;
                Vector2 l_foot2 = PlayerBones[p.userID.ToString()].l_foot;
                Vector2 r_foot2 = PlayerBones[p.userID.ToString()].r_foot;


                Drawing.DrawLine(head2, spine2, color, 1.2f, true);
                Drawing.DrawLine(spine2, l_upper2, color, 1.2f, true);
                Drawing.DrawLine(l_upper2, l_fore2, color, 1.2f, true);
                Drawing.DrawLine(l_fore2, l_hand2, color, 1.2f, true);
                Drawing.DrawLine(spine2, r_upper2, color, 1.2f, true);
                Drawing.DrawLine(r_upper2, r_fore2, color, 1.2f, true);
                Drawing.DrawLine(r_fore2, r_hand2, color, 1.2f, true);
                Drawing.DrawLine(spine2, pelvis2, color, 1.2f, true);
                Drawing.DrawLine(pelvis2, l_hip2, color, 1.2f, true);
                Drawing.DrawLine(l_hip2, l_knee2, color, 1.2f, true);
                Drawing.DrawLine(l_knee2, l_foot2, color, 1.2f, true);
                Drawing.DrawLine(pelvis2, r_hip2, color, 1.2f, true);
                Drawing.DrawLine(r_hip2, r_knee2, color, 1.2f, true);
                Drawing.DrawLine(r_knee2, r_foot2, color, 1.2f, true);
            }
        }
        void OnGUI()
        {
            GUI.skin = skin;
            if (EnableMenu)
            {
                rc = GUI.Window(0, rc, new GUI.WindowFunction(GI), "UFO HACKS: " + DateTime.Now.ToString());
            }

            if(EnableHack)
            {
                if(EnableESP)
                {
                    if(Players || Sleeper)
                    {
                        Visual.Players();
                        if (Skelet)
                        {
                            foreach (BasePlayer pl in BasePlayer.VisiblePlayerList)
                            {
                                if (pl != null && pl != LocalPlayer.Entity && !pl.IsSleeping())
                                {
                                    DrawBones(pl, Color.white);
                                }
                            }
                        }
                        if (Radar)
                        {
                            Renders.DrawRadarBackground(new Rect(30, 200, 200, 200));
                            Renders.BoxRect(new Rect(30 + (200 / 2) - 3, 200 + (200 / 2) - 3, 6f, 6f), UnityEngine.Color.cyan);


                            foreach (BasePlayer p in BasePlayer.VisiblePlayerList)
                            {
                                if (p != null && p.health > 0f && !p.IsSleeping() && !p.IsLocalPlayer())
                                {

                                    if (Players)
                                    {

                                        Vector3 centerPos = LocalPlayer.Entity.transform.position;
                                        Vector3 extPos = p.transform.position;

                                        float dist = Vector3.Distance(centerPos, extPos);

                                        float dx = centerPos.x - extPos.x;
                                        float dz = centerPos.z - extPos.z;

                                        float deltay = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg - 270 - LocalPlayer.Entity.transform.eulerAngles.y;

                                        float bX = dist * Mathf.Cos(deltay * Mathf.Deg2Rad);
                                        float bY = dist * Mathf.Sin(deltay * Mathf.Deg2Rad);

                                        bX = bX * ((float)200 / (float)radardist) / 2f;
                                        bY = bY * ((float)200 / (float)radardist) / 2f;

                                        if (dist <= radardist)
                                        {
                                            Renders.BoxRect(new Rect(30 + (200 / 2) + bX - 3, 200 + (200 / 2) + bY - 3, 6f, 6f), Color.white);
                                        }

                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        public static void GI(int id)
        {
            GUI.Box(new Rect(0, 18, 700, 332), "", skin.GetStyle("back"));
            GUI.Box(new Rect(572, 40, 128, 64), "", skin.GetStyle("logo"));
            EnableESP = GUI.Toggle(new Rect(30, 40, 200, 24), EnableESP, "ESP");
            Players = GUI.Toggle(new Rect(30, 70, 120, 24), Players, "PLAYERS");
            Sleeper = GUI.Toggle(new Rect(30, 100, 200, 24), Sleeper, "SLEEPERS");
            Nicknames = GUI.Toggle(new Rect(30, 130, 200, 24), Nicknames, "NICKNAMES");
            Health = GUI.Toggle(new Rect(30, 160, 200, 24), Health, "HEALTH");
            Distance = GUI.Toggle(new Rect(30, 190, 200, 24), Distance, "DISTANCE");
            Skelet = GUI.Toggle(new Rect(30, 220, 200, 24), Skelet, "SKELET");
            Tracers = GUI.Toggle(new Rect(30, 250, 200, 24), Tracers, "TRACERS");
            Radar = GUI.Toggle(new Rect(30, 280, 200, 24), Radar, "RADAR");
            GUI.Label(new Rect(200, 40, 140, 18), String.Format("PL DIST: {0}", (int)playerdist));
            playerdist = GUI.HorizontalSlider(new Rect(200, 80, 140, 24), playerdist, 0, 500);
            GUI.Label(new Rect(200, 110, 140, 18), String.Format("SL DIST: {0}", (int)sleepdist));
            sleepdist = GUI.HorizontalSlider(new Rect(200, 150, 140, 24), sleepdist, 0, 500);
            GUI.Label(new Rect(200, 180, 140, 18), String.Format("RD DIST: {0}", (int)radardist));
            radardist = GUI.HorizontalSlider(new Rect(200, 220, 140, 24), radardist, 0, 500);

            string espbutton = String.Format("ESP Key: {0}", ESPkey);
            string menubutton = String.Format("Menu Key: {0}", MenuKEY);
            KeyESP = GUI.Toggle(new Rect(350, 40, 100, 24), KeyESP, "ESP KEY");
            if (GUI.Button(new Rect(350, 70, 140, 24), espbutton))
                isKey = !isKey;
            if (GUI.Button(new Rect(350, 110, 140, 24), menubutton))
                isMKey = !isMKey;

            if (isKey)
            {
                if (!isMKey)
                {
                    if (Event.current.isKey && Event.current.type == EventType.KeyDown)
                    {
                        if (Event.current.keyCode.ToString() != "None")
                        {
                            ESPkey = Event.current.keyCode;
                            espbutton = String.Format("Key: {0}", ESPkey);
                            isKey = false;
                        }
                    }
                }
            }

            if (isMKey)
            {
                if (!isKey)
                {
                    if (Event.current.isKey && Event.current.type == EventType.KeyDown)
                    {
                        if (Event.current.keyCode.ToString() != "None")
                        {
                            MenuKEY = Event.current.keyCode;
                            espbutton = String.Format("Menu Key: {0}", MenuKEY);
                            isMKey = false;
                        }
                    }
                }
            }

            GUI.DragWindow();
        }

        public static Rect rc = new Rect(200, 200, 700, 350);
    }
}
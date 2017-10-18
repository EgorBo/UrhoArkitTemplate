using ARKitXamarinDemo;
using System;
using System.Collections.Generic;
using System.Text;
using Urho;

namespace UrhoArkitDemo
{
	public class UrhoApp : ArkitApp
	{
		Node bookshelfNode;
		bool scaling;

		[Preserve]
		public UrhoApp(ApplicationOptions opts) : base(opts) { }

		protected override unsafe void Start()
		{
			base.Start();

			bookshelfNode = Scene.InstantiateXml(
				source: ResourceCache.GetFile("Objects/Scene.xml"),
				position: new Vector3(0, -1f, 1f),
				rotation: new Quaternion(0, 90, 0));
			bookshelfNode.SetScale(0.5f);


			Input.TouchBegin += OnTouchBegin;
			Input.TouchEnd += OnTouchEnd;

			UnhandledException += UrhoApp_UnhandledException;
		}

		void UrhoApp_UnhandledException(object sender, Urho.UnhandledExceptionEventArgs e)
		{

		}

		void OnTouchBegin(TouchBeginEventArgs e)
		{
			scaling = false;
		}

		void OnTouchEnd(TouchEndEventArgs e)
		{
			if (scaling)
				return;

			var pos = HitTest(e.X / (float)Graphics.Width, e.Y / (float)Graphics.Height);
			if (pos != null)
				bookshelfNode.Position = pos.Value;
		}

		protected override void OnUpdate(float timeStep)
		{
			// Scale up\down
			if (Input.NumTouches == 2)
			{
				scaling = true;
				var state1 = Input.GetTouch(0);
				var state2 = Input.GetTouch(1);
				var distance1 = IntVector2.Distance(state1.Position, state2.Position);
				var distance2 = IntVector2.Distance(state1.LastPosition, state2.LastPosition);
				bookshelfNode.SetScale(bookshelfNode.Scale.X + (distance1 - distance2) / 10000f);
			}

			base.OnUpdate(timeStep);
		}
	}
}
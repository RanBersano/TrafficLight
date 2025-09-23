using TraficLight.ModelsLogic;

namespace TraficLight.Models
{
    internal abstract class TraficLightModel
    {
        public enum TraficLightState
        {
            Red,
            RedYellow,
            Yellow,
            Green
        }
        public enum TraficLight
        {
            Red,
            Yellow,
            Green
        }
        protected System.Timers.Timer timer = new (1000);
        protected bool isAutoChange = false;
        protected TraficLightState currentState = TraficLightState.Red;
        protected Light[] lights = [new Light(Colors.Red,true), new Light(Colors.Yellow, false), new Light(Colors.Green, false)];
        public EventHandler<LightChengedEventArgs>? LightChanged;
        public Color RedColor => lights[(int)TraficLight.Red].Color;
        public Color YellowColor => lights[(int)TraficLight.Yellow].Color;
        public Color GreenColor => lights[(int)TraficLight.Green].Color;
        public abstract string SwitchChangeLightText { get; } 
        public abstract string LightImage {  get; }
        public abstract void ChangeLight();
        public abstract void SwitchAutoChange();
        protected LightImage lightImage = new ();
        public abstract string SecondsSwitch { get; set; }
        public abstract void ChangeSeconds();
    }
}

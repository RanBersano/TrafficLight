using System.Timers;
using TraficLight.Models;

namespace TraficLight.ModelsLogic
{
    internal class TraficLight : TraficLightModel
    {
        public override string LightImage => lightImage.GetLightImage(currentState);

        public override string SwitchChangeLightText => isAutoChange ? Strings.StopAutiChangeLightText : Strings.StartAutiChangeLightText;
        public override double SecondsSwitch { get => timer.Interval / SecondsToMiliseconds; set => timer.Interval = value * SecondsToMiliseconds; }
        public TraficLight()
        {
            timer.Elapsed += OnTimerElapsed;
        }
        public override void ChangeSeconds()
        {
            timer.Interval = SecondsSwitch * SecondsToMiliseconds;
        }
        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            ChangeLight();
        }
        public override void ChangeLight()
        {
            if (currentState == TraficLightState.Red)
            {
                currentState = TraficLightState.RedYellow;
                lights[(int)TraficLight.Yellow].IsOn = true;
                LightChanged?.Invoke(this, new LightChengedEventArgs(TraficLight.Yellow));
            }
            else if (currentState == TraficLightState.RedYellow)
            {
                currentState = TraficLightState.Green;
                lights[(int)TraficLight.Red].IsOn = false;
                lights[(int)TraficLight.Yellow].IsOn = false;
                lights[(int)TraficLight.Green].IsOn = true;
                foreach(TraficLight tl in Enum.GetValues<TraficLight>())
                    LightChanged?.Invoke(this, new LightChengedEventArgs(tl));
            }
            else if (currentState == TraficLightState.Green)
            {
                currentState = TraficLightState.Yellow;
                lights[(int)TraficLight.Green].IsOn = false;
                lights[(int)TraficLight.Yellow].IsOn = true;
                foreach (TraficLight tl in Enum.GetValues<TraficLight>())
                    if(tl != TraficLight.Red)
                        LightChanged?.Invoke(this, new LightChengedEventArgs(tl));
            }
            else if (currentState == TraficLightState.Yellow)
            {
                currentState = TraficLightState.Red;
                lights[(int)TraficLight.Red].IsOn = true;
                lights[(int)TraficLight.Yellow].IsOn = false;
                foreach (TraficLight tl in Enum.GetValues<TraficLight>())
                    if (tl != TraficLight.Green)
                        LightChanged?.Invoke(this, new LightChengedEventArgs(tl));
            }
        }
        public override void SwitchAutoChange()
        {
            isAutoChange = !isAutoChange;
            if (isAutoChange)
                timer.Start();
            else
                timer.Stop();
        }
    }
}

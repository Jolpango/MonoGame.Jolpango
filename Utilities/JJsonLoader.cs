using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Jolpango.Graphics.Dispersion;
using MonoGame.Jolpango.Graphics.Particles;
using MonoGame.Jolpango.Graphics.Transitions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MonoGame.Jolpango.Utilities
{
    public class JJsonLoader
    {
        public static ParticleEmitter ReadParticleEmitterFromFile(string path, Game game = null)
        {
            JObject o = JObject.Parse(File.ReadAllText(path));
            string? texturePath = (string)o.Root["texture"] ?? null;
            Texture2D texture;
            if (game is not null && texturePath is not null)
            {
                texture = game.Content.Load<Texture2D>(texturePath);
            }
            else
            {
                texture = new Texture2D(game.GraphicsDevice, 2, 2);
                Color[] data = new Color[2 * 2];
                for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
                    texture.SetData(data);
            }
            IParticleTransition[] transitions = ReadTransitions(o);
            IDispersionMethod dispersionMethod = ReadDispersionMethod(o);
            ParticleEmitter emitter = new ParticleEmitter(texture, dispersionMethod, transitions)
            {
                Easing = ReadEasingFunction(o),
                MinRadius = (int?)o.Root["radius"]["min"] ?? 1,
                MaxRadius = (int?)o.Root["radius"]["max"] ?? 1,
                TimeToLive = (float?)o.Root["timeToLive"] ?? 1
            };
            return emitter;
        }
        private static EasingFunction ReadEasingFunction(JObject o)
        {
            string easingString = (string)o.Root["easing"];
            EasingFunction e = (EasingFunction)Enum.Parse(typeof(EasingFunction), easingString);
            return e;
        }
        private static IParticleTransition[] ReadTransitions(JObject o)
        {
            var transitionO = o.Root["transitions"];
            List<IParticleTransition> transitions = new List<IParticleTransition>();
            foreach (var transition in transitionO)
            {
                if ((string)transition["type"] == "ColorTransition")
                {
                    transitions.Add(ReadColorTransition(transition));
                }
                else if ((string)transition["type"] == "AlphaTransition")
                {
                    transitions.Add(ReadAlphaTransition(transition));
                }
                else if ((string)transition["type"] == "ScaleTransition")
                {
                    transitions.Add(ReadScaleTransition(transition));
                }
            }
            return transitions.ToArray();
        }
        private static AlphaTransition ReadAlphaTransition(JToken transition)
        {
            AlphaTransition alphaTransition = new AlphaTransition()
            {
                StartAlpha = (float)transition["alpha"]["start"],
                EndAlpha = (float)transition["alpha"]["end"]
            };
            return alphaTransition;
        }
        private static ScaleTransition ReadScaleTransition(JToken transition)
        {
            ScaleTransition scaleTransition = new ScaleTransition()
            {
                StartScale = (float)transition["scale"]["start"],
                EndScale = (float)transition["scale"]["end"]
            };
            return scaleTransition;
        }
        private static ColorTransition ReadColorTransition(JToken transition)
        {
            ColorTransition colorTransition = new ColorTransition();
            List<Color> colorList = new List<Color>();
            foreach (var color in transition["colors"])
            {
                Color c;
                if ((string)color["colorName"] is not null)
                {
                    c = JExtras.ColorDictionary[(string)color["colorName"]];
                }
                else
                {
                    c = new Color((int)color["r"], (int)color["g"], (int)color["b"]);
                }
                colorList.Add(c);
            }
            colorTransition.Colors = colorList.ToArray();
            return colorTransition;
        }

        private static IDispersionMethod ReadDispersionMethod(JObject o)
        {
            var dispersionO = o.Root["dispersionMethod"];
            if ((string)dispersionO["type"] == "DispersionCone")
            {
                return ReadDispersionCone(dispersionO);
            }
            else if ((string)dispersionO["type"] == "DispersionInverseCone")
            {
                return ReadDispersionInverseCone(dispersionO);
            }
            else if ((string)dispersionO["type"] == "DispersionRandom")
            {
                return ReadDispersionRandom(dispersionO);
            }
            return null;
        }
        private static DispersionRandom ReadDispersionRandom(JToken disperionO)
        {
            DispersionRandom dispersion = new DispersionRandom((float)disperionO["length"]["min"], (float)disperionO["length"]["max"]);
            return dispersion;
        }
        private static DispersionInverseCone ReadDispersionInverseCone(JToken disperionO)
        {
            DispersionInverseCone dispersion = new DispersionInverseCone(
                new Vector2((float)disperionO["direction"]["x"], (float)disperionO["direction"]["y"]),
                (float)disperionO["radius"],
                (float)disperionO["length"]);
            return dispersion;
        }
        private static DispersionCone ReadDispersionCone(JToken disperionO)
        {
            DispersionCone dispersion = new DispersionCone(
                new Vector2((float)disperionO["direction"]["x"], (float)disperionO["direction"]["y"]),
                (float)disperionO["angle"],
                (float)disperionO["length"]["min"],
                (float)disperionO["length"]["max"]);
            return dispersion;
        }
    }
}

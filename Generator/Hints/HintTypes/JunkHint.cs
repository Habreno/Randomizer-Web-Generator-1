namespace TPRandomizer.Hints
{
    using System;
    using System.Collections.Generic;
    using TPRandomizer.Util;

    public class JunkHint : Hint
    {
        public override HintType type { get; } = HintType.Junk;

        public ushort idValue { get; private set; }
        public bool indicatesBarren { get; private set; }

        public JunkHint(Random rnd, bool indicatesBarren = false)
        {
            this.idValue = (ushort)rnd.Next(ushort.MaxValue);
            this.indicatesBarren = indicatesBarren;
        }

        private JunkHint(ushort idValue, bool indicatesBarren = false)
        {
            this.idValue = idValue;
            this.indicatesBarren = indicatesBarren;
        }

        public override List<HintText> toHintTextList()
        {
            HintText hintText = new HintText();
            hintText.text = $"junk hint num'{idValue}'";
            if (indicatesBarren)
                hintText.text += " (barren zone)";
            return new List<HintText> { hintText };
        }

        public override string encodeAsBits(HintEncodingBitLengths bitLengths)
        {
            string result = base.encodeAsBits(bitLengths);
            result += SettingsEncoder.EncodeNumAsBits(idValue, 16);
            result += indicatesBarren ? "1" : "0";
            return result;
        }

        public static JunkHint decode(
            HintEncodingBitLengths bitLengths,
            BitsProcessor processor,
            Dictionary<int, byte> itemPlacements
        )
        {
            ushort idValue = (ushort)processor.NextInt(16);
            bool indicatesBarren = processor.NextBool();
            return new JunkHint(idValue, indicatesBarren);
        }
    }
}

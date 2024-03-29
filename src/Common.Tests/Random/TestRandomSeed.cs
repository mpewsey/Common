﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPewsey.Common.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MPewsey.Common.Random.Tests
{
    [TestClass]
    public class TestRandomSeed
    {
        [TestMethod]
        public void TestSaveAndLoad()
        {
            var path = "RandomSeed.xml";
            var seed = new RandomSeed(12345);
            XmlSerialization.SaveXml(path, seed);
            var copy = XmlSerialization.LoadXml<RandomSeed>(path);
            Assert.AreEqual(seed.Seed, copy.Seed);

            for (int i = 0; i < 1000; i++)
            {
                Assert.AreEqual(seed.Next(), copy.Next());
            }
        }

        [TestMethod]
        public void TestDrawWeightedIndexDouble()
        {
            var seed = new RandomSeed();
            var values = new double[] { 0, 1, 0 };
            Assert.AreEqual(1, seed.DrawWeightedIndex(values));
            Assert.AreEqual(1, seed.DrawWeightedIndex(values, new List<double>()));
            Assert.AreEqual(-1, seed.DrawWeightedIndex(Array.Empty<double>()));
        }

        [TestMethod]
        public void TestDrawWeightedIndexesDoubleWithReplacement()
        {
            var seed = new RandomSeed();
            var values = new double[] { 0, 1, 0 };
            var result = seed.DrawWeightedIndexes(values, 10, true);
            var expected = Enumerable.Repeat(1, 10).ToList();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDrawWeightedIndexesDoubleWithoutReplacement()
        {
            var seed = new RandomSeed();
            var values = new double[] { 0, 1, 0 };
            var result = seed.DrawWeightedIndexes(values, 10, false);
            var expected = Enumerable.Repeat(1, 1).ToList();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDrawWeightedIndexFloat()
        {
            var seed = new RandomSeed();
            var values = new float[] { 0, 1, 0 };
            Assert.AreEqual(1, seed.DrawWeightedIndex(values));
            Assert.AreEqual(1, seed.DrawWeightedIndex(values, new List<double>()));
            Assert.AreEqual(-1, seed.DrawWeightedIndex(Array.Empty<float>()));
        }

        [TestMethod]
        public void TestDrawWeightedIndexesFloatWithReplacement()
        {
            var seed = new RandomSeed();
            var values = new float[] { 0, 1, 0 };
            var result = seed.DrawWeightedIndexes(values, 10, true);
            var expected = Enumerable.Repeat(1, 10).ToList();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDrawWeightedIndexesFloatWithoutReplacement()
        {
            var seed = new RandomSeed();
            var values = new float[] { 0, 1, 0 };
            var result = seed.DrawWeightedIndexes(values, 10, false);
            var expected = Enumerable.Repeat(1, 1).ToList();
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNext()
        {
            var expected = new List<int>()
            {
                143337951, 150666398, 1663795458, 1097663221, 1712597933,
                1776631026, 356393799, 1580828476, 558810388, 1086637143,
                494509053, 831377771, 463814839, 44691035, 289552956,
                1590924033, 418954878, 1904902962, 1849199486, 770656856,
                222698908, 1137270943, 770420532, 1519356451, 1246560209,
                1332617375, 1573024538, 1606065954, 850942673, 526685912,
                1473914819, 452144508, 1111403504, 1042369160, 542895576,
                1655234974, 5538230, 1039193352, 961982272, 1044665811,
                1528100810, 969047112, 579718272, 607824875, 1364170491,
                633032322, 793567355, 1831117809, 377238926, 1830086762,
                1383740914, 1322492187, 948158774, 1066648348, 64646849,
                1153550655, 1527729513, 144439007, 1998586659, 379980558,
                203606488, 897811492, 729885803, 32124476, 1760205971,
                42364545, 1867457914, 1568929326, 1649279106, 781801629,
                1585385803, 1527245173, 942920690, 804533675, 1390039693,
                1401135443, 557552671, 162595657, 155185960, 613527887,
                539050020, 1889390376, 1228827028, 1168339558, 1290428645,
                151422632, 1651469381, 44755156, 977722311, 1536828568,
                127505461, 2008582870, 1188090340, 582001714, 841059323,
                630289318, 239161309, 547593796, 995102551, 1321805946,
            };

            var seed = new RandomSeed(12345);
            var result = new List<int>(expected.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                result.Add(seed.Next());
            }

            CollectionAssert.AreEqual(expected, result);

            // Check that SetSeed properly resets the randomizer.
            seed.SetSeed(12345);
            result.Clear();

            for (int i = 0; i < expected.Count; i++)
            {
                result.Add(seed.Next());
            }

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNextDouble()
        {
            var expected = new List<double>()
            {
                0.0667469348137951, 0.0701595088793708, 0.774765135149828, 0.511139268759237, 0.797490558492714,
                0.827308291023275, 0.165958795308116, 0.736130623489679, 0.26021636475819, 0.506004851081411,
                0.230273722312541, 0.387140443263175, 0.215980615101746, 0.0208108848989061, 0.134833602297508,
                0.740831733560577, 0.195091067904183, 0.887039565894306, 0.861100613540551, 0.358865063804605,
                0.103702260229598, 0.529583051581673, 0.358755016866492, 0.707505481181436, 0.580474831899849,
                0.620548322620126, 0.732496631672837, 0.747882739989032, 0.396251060718787, 0.245257239902977,
                0.686345072317098, 0.210546193742448, 0.517537586631969, 0.485390965121515, 0.252805452911558,
                0.770778849148554, 0.00257893931240725, 0.483912114279304, 0.44795790335534, 0.486460426583169,
                0.711577390651953, 0.451247725845896, 0.269952356941044, 0.283040513881967, 0.635241387242098,
                0.294778646107194, 0.369533596266775, 0.852680676548127, 0.17566556398555, 0.852200557874609,
                0.644354575613679, 0.615833414539617, 0.441520835478567, 0.496696843065646, 0.0301035349397471,
                0.537163883232122, 0.711404492012879, 0.0672596539683918, 0.930664436859388, 0.176942235872588,
                0.0948116593504379, 0.418076055319084, 0.339879562770892, 0.0149591248552125, 0.819659778764313,
                0.019727528570093, 0.869602856631206, 0.730589649980231, 0.768005431987348, 0.364054753148954,
                0.73825279424817, 0.711178953624879, 0.439081662538965, 0.374640186957382, 0.647287673152651,
                0.652454534383702, 0.259630694640628, 0.0757145029845249, 0.0722640939393379, 0.285696185792655,
                0.25101472635335, 0.87981595512471, 0.572217176003483, 0.544050502844178, 0.600902664289299,
                0.0705116577774806, 0.769025358263881, 0.0208407435663234, 0.455287430181768, 0.715641569679436,
                0.0593743571356751, 0.935319285344015, 0.553247677419916, 0.271015667482752, 0.391648767232731,
                0.293501335332869, 0.111368163075004, 0.254993232085832, 0.463380735117654, 0.615513858672005,
            };

            var seed = new RandomSeed(12345);
            var result = new List<double>(expected.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                result.Add(Math.Round(seed.NextDouble(), 12));
                expected[i] = Math.Round(expected[i], 12);
            }

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNextMaxValue()
        {
            var expected = new List<int>()
            {
                667, 701, 7747, 5111, 7974,
                8273, 1659, 7361, 2602, 5060,
                2302, 3871, 2159, 208, 1348,
                7408, 1950, 8870, 8611, 3588,
                1037, 5295, 3587, 7075, 5804,
                6205, 7324, 7478, 3962, 2452,
                6863, 2105, 5175, 4853, 2528,
                7707, 25, 4839, 4479, 4864,
                7115, 4512, 2699, 2830, 6352,
                2947, 3695, 8526, 1756, 8522,
                6443, 6158, 4415, 4966, 301,
                5371, 7114, 672, 9306, 1769,
                948, 4180, 3398, 149, 8196,
                197, 8696, 7305, 7680, 3640,
                7382, 7111, 4390, 3746, 6472,
                6524, 2596, 757, 722, 2856,
                2510, 8798, 5722, 5440, 6009,
                705, 7690, 208, 4552, 7156,
                593, 9353, 5532, 2710, 3916,
                2935, 1113, 2549, 4633, 6155,
            };

            var seed = new RandomSeed(12345);
            var result = new List<int>(expected.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                result.Add(seed.Next(10000));
            }

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNextLargeRange()
        {
            var expected = new List<int>()
            {
                -143337953, 1663795457, -1712597935, -356393801, 558810387,
                494509052, 463814838, 289552955, -418954880, -1849199488,
                222698907, 770420531, 1246560208, -1573024540, -850942675,
                -1473914821, -1111403506, -542895578, -5538232, 961982271,
                -1528100812, 579718271, -1364170493, 793567354, -377238928,
                1383740913, -948158776, 64646848, 1527729512, -1998586661,
                -203606490, -729885805, 1760205970, -1867457916, 1649279105,
                1585385802, 942920689, 1390039692, 557552670, 155185959,
                -539050022, -1228827030, -1290428647, -1651469383, -977722313,
                -127505463, -1188090342, -841059325, -239161311, -995102553,
                -913058057, -181838705, -244700961, 379571496, 1824092301,
                -595997986, -2136736696, 1988414184, -1816468113, 889179477,
                -538378813, -591207017, -654296170, -339154835, 2110957998,
                1161974133, 1314976752, 1847953478, 1707551672, -923638601,
                -1919334784, -368146503, -940830586, 2019329822, -741071178,
                -961304856, 1805897964, -783427137, 780914660, -1088624778,
                1664824554, 180150746, 1796504565, 50157102, -1684588942,
                -901631735, 785391292, -1836932206, 1454556256, 1319103600,
                1852440403, 1767331446, -1945929170, 351544556, 197436525,
                -533497332, 544764954, 1916591995, 1327693534, 517706338,
            };

            var seed = new RandomSeed(12345);
            var result = new List<int>(expected.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                result.Add(seed.Next(int.MinValue, int.MaxValue));
            }

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNextRange()
        {
            var expected = new List<int>()
            {
                5333, 5350, 8873, 7555, 8987,
                9136, 5829, 8680, 6301, 7530,
                6151, 6935, 6079, 5104, 5674,
                8704, 5975, 9435, 9305, 6794,
                5518, 7647, 6793, 8537, 7902,
                8102, 8662, 8739, 6981, 6226,
                8431, 6052, 7587, 7426, 6264,
                8853, 5012, 7419, 7239, 7432,
                8557, 7256, 6349, 6415, 8176,
                6473, 6847, 9263, 5878, 9261,
                8221, 8079, 7207, 7483, 5150,
                7685, 8557, 5336, 9653, 5884,
                5474, 7090, 6699, 5074, 9098,
                5098, 9348, 8652, 8840, 6820,
                8691, 8555, 7195, 6873, 8236,
                8262, 6298, 5378, 5361, 6428,
                6255, 9399, 7861, 7720, 8004,
                5352, 8845, 5104, 7276, 8578,
                5296, 9676, 7766, 6355, 6958,
                6467, 5556, 6274, 7316, 8077,
            };

            var seed = new RandomSeed(12345);
            var result = new List<int>(expected.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                result.Add(seed.Next(5000, 10000));
            }

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNextFloat()
        {
            var seed = new RandomSeed(12345);

            for (int i = 0; i < 1_000_000; i++)
            {
                var value = seed.NextFloat();
                Assert.IsTrue(value >= 0);
                Assert.IsTrue(value < 1);
            }
        }

        [TestMethod]
        public void TestNextFloatMaxValue()
        {
            var seed = new RandomSeed(12345);

            for (int i = 0; i < 1_000_000; i++)
            {
                var value = seed.NextFloat(200);
                Assert.IsTrue(value >= 0);
                Assert.IsTrue(value < 200);
            }
        }

        [TestMethod]
        public void TestNextFloatRange()
        {
            var seed = new RandomSeed(12345);

            for (int i = 0; i < 1_000_000; i++)
            {
                var value = seed.NextFloat(100, 200);
                Assert.IsTrue(value >= 100);
                Assert.IsTrue(value < 200);
            }
        }

        [TestMethod]
        public void TestNextDoubleMaxValue()
        {
            var seed = new RandomSeed(12345);

            for (int i = 0; i < 1_000_000; i++)
            {
                var value = seed.NextDouble(200);
                Assert.IsTrue(value >= 0);
                Assert.IsTrue(value < 200);
            }
        }

        [TestMethod]
        public void TestNextDoubleRange()
        {
            var seed = new RandomSeed(12345);

            for (int i = 0; i < 1_000_000; i++)
            {
                var value = seed.NextDouble(100, 200);
                Assert.IsTrue(value >= 100);
                Assert.IsTrue(value < 200);
            }
        }

        [TestMethod]
        public void TestToString()
        {
            var seed = new RandomSeed(12345);
            Assert.IsTrue(seed.ToString().StartsWith("RandomSeed("));
        }

        [TestMethod]
        public void TestCopy()
        {
            var seed = new RandomSeed(12345);
            var copy = seed.Copy();

            for (int i = 0; i < 1000; i++)
            {
                Assert.AreEqual(seed.Next(), copy.Next());
            }
        }

        [TestMethod]
        public void TestCurrent()
        {
            Assert.IsFalse(RandomSeed.Current.ChanceSatisfied(0));
        }

        [TestMethod]
        public void TestChanceSatisfied()
        {
            var seed = new RandomSeed(12345);
            var results = new bool[1000];

            for (int i = 0; i < 1000; i++)
            {
                Assert.IsFalse(seed.ChanceSatisfied(0.0f));
            }

            for (int i = 0; i < 1000; i++)
            {
                Assert.IsTrue(seed.ChanceSatisfied(1.0f));
            }

            for (int i = 0; i < results.Length; i++)
            {
                results[i] = seed.ChanceSatisfied(0.5f);
            }

            Assert.IsTrue(results.Contains(true));
            Assert.IsTrue(results.Contains(false));
        }

        [TestMethod]
        public void TestDoubleInsideUnitCircle()
        {
            var seed = new RandomSeed(12345);

            for (int i = 0; i < 1_000_000; i++)
            {
                var (x, y) = seed.DoubleInsideUnitCircle();
                Assert.IsTrue(Math.Abs(x) <= 1);
                Assert.IsTrue(Math.Abs(y) <= 1);
            }
        }

        [TestMethod]
        public void TestFloatInsideUnitCircle()
        {
            var seed = new RandomSeed(12345);

            for (int i = 0; i < 1_000_000; i++)
            {
                var (x, y) = seed.FloatInsideUnitCircle();
                Assert.IsTrue(Math.Abs(x) <= 1);
                Assert.IsTrue(Math.Abs(y) <= 1);
            }
        }
    }
}
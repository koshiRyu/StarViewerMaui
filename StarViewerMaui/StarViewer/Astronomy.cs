using System;
using System.Collections.Generic;
using System.Text;

namespace StarViewerMaui.StarViewer
{
    /// <summary>
    ///     天文計算クラス
    /// </summary>
    public class Astronomy
    {
        /// <summary>
        ///     日本標準時（世界時との時差）Ver.3.11
        /// </summary>
        protected internal const double TDIFF = 9.0;

        /// <summary>
        ///     平均軌道要素
        /// </summary>
        protected internal readonly Dictionary<Planets, List<double>> PlanetOrbitalElements = new Dictionary<Planets, List<double>>
        {
            {
                Planets.Mercury, new List<double>()
                {
                    252.2509,   +4.0932377062,  +0.000303,		//Ｌ 平均黄経
                    77.4561,    +1.556401,      +0.000295,		//ω 近日点黄経
                    48.3309,    +1.186112,      +0.000175,		//Ω 昇交点黄経
                    7.0050,     +0.001821,						//ｉ 軌道傾角
                    0.205632,   +0.00002040,					//ｅ 軌道離心率
                    +0.387098,  0.0								//ａ 軌道長半径
                }
            },
            {
                Planets.Venus, new List<double>()
                {
                    181.9798,   +1.602168732,   +0.000310,		//Ｌ 平均黄経
                    131.5637,   +1.402152,      -0.001076,		//ω 近日点黄経
                    76.6799,    +0.901044,      +0.000406,		//Ω 昇交点黄経
                    3.3947,     +0.001004,						//ｉ 軌道傾角
                    0.006772,   -0.00004778,					//ｅ 軌道離心率
                    0.723330,   0.0								//ａ 軌道長半径
                }
            },
            {
                Planets.Mars , new List<double>()
                {
                    355.4330,   +0.524071085,   +0.000311,		//Ｌ 平均黄経
                    336.0602,   +1.840968,      +0.000135,		//ω 近日点黄経
                    49.5581,    +0.772019,      +0.0,			//Ω 昇交点黄経
                    1.8497,     -0.000601,						//ｉ 軌道傾角
                    0.093401,   +0.00009048,					//ｅ 軌道離心率
                    +1.523679,  0.0								//ａ 軌道長半径
                }
            },
            {
                Planets.Jupiter , new List<double>()
                {
                    34.3515,    +0.083129439,   +0.000223,		//Ｌ 平均黄経
                    14.3312,    +1.612635,      +0.001030,		//ω 近日点黄経
                    100.4644,   +1.020977,      +0.000403,		//Ω 昇交点黄経
                    1.3033,     -0.005496,						//ｉ 軌道傾角
                    0.048498,   +0.00016323,					//ｅ 軌道離心率
                    5.202603,   0.0								//ａ 軌道長半径
                }
            },
            {
                Planets.Saturn , new List<double>()
                {
                    50.0774,    +0.033497907,   +0.000519,		//Ｌ 平均黄経
                    93.0572,    +1.963761,      +0.000838,		//ω 近日点黄経
                    113.665,    +0.877088,      -0.000121,		//Ω 昇交点黄経
                    2.4889,     -0.003736,						//ｉ 軌道傾角
                    0.055548,   -0.00034664,					//ｅ 軌道離心率
                    9.554909,   -0.0000021						//ａ 軌道長半径
                }
            },
            {
                Planets.Uranus , new List<double>()
                {
                    314.0550,   +0.011769036,   +0.000304,		//Ｌ 平均黄経
                    173.0053,   +1.486378,      +0.000214,		//ω 近日点黄経
                    74.0060,    +0.521127,      +0.001339,		//Ω 昇交点黄経
                    0.7732,     +0.000774,						//ｉ 軌道傾角
                    0.046381,   -0.00002729,					//ｅ 軌道離心率
                    19.218446,  -0.000003						//ａ 軌道長半径
                }
            },
            {
                Planets.Neptune , new List<double>()
                {
                    304.3487,   +0.006020077,   +0.000309,		//Ｌ 平均黄経
                    48.1203,    +1.426296,      +0.000384,		//ω 近日点黄経
                    131.784,    +1.102204,      +0.000260,		//Ω 昇交点黄経
                    1.7700,     -0.009308,						//ｉ 軌道傾角
                    0.009456,   +0.00000603,					//ｅ 軌道離心率
                    30.110387,  0.0								//ａ 軌道長半径
                }
            },
            {
                Planets.Pluto , new List<double>
                {
                    238.4670,   +0.00401596,    -0.0091,		//Ｌ 平均黄経
                    224.1416,   +1.3901,        +0.0003,		//ω 近日点黄経
                    110.3182,   +1.3507,        +0.0004,		//Ω 昇交点黄経
                    17.1451,    -0.0055,						//ｉ 軌道傾角
                    0.249005,   +0.000039,						//ｅ 軌道離心率
                    39.540343,  +0.003131						//ａ 軌道長半径
                }
            }
        };

        /// <summary>
        ///     ケプラー運動方程式の解法（漸化法）
        /// </summary>
        /// <param name="l">平均近点離角</param>
        /// <param name="e">軌道離心率</param>
        /// <returns>離心近点離角</returns>
        protected internal double __kepler(double l, double e)
        {
            double u0 = l;
            double u1;
            double du;
            do
            {
                du = (l - u0 + rad2deg(e * Math.Sin(deg2rad(u0)))) / (1 - e * Math.Cos(deg2rad(u0)));
                u1 = u0 + du;
                u0 = u1;
            } while (Math.Abs(du) < 1e-15);

            return u1;
        }

        /// <summary>
        ///     惑星の日心黄道座標を計算
        /// </summary>
        /// <param name="planet">惑星名</param>
        /// <param name="year">グレゴリオ暦年</param>
        /// <param name="month">グレゴリオ暦月</param>
        /// <param name="day">グレゴリオ暦日</param>
        /// <param name="hour">時（世界時）</param>
        /// <param name="min">分（世界時）</param>
        /// <param name="sec">秒（世界時）</param>
        /// <returns>array(黄経,黄緯,動径)／null：惑星名の間違い</returns>
        protected internal double[] zodiacSun(Planets planet, int year, int month, int day, double hour, double min, double sec)
        {
            //平均軌道要素
            if (!this.PlanetOrbitalElements.ContainsKey(planet)) return null;
            List<double> tbl = this.PlanetOrbitalElements[planet];

            //計算開始
            double d = this.Gregorian2JD(year, month, day, hour, min, sec) - 2451544.5;
            double T = d / 36525.0;

            //平均軌道要素
            double L = this.__angle(tbl[0] + tbl[1] * d + tbl[2] * Math.Pow(T, 2));
            double omega = this.__angle(tbl[3] + tbl[4] * T + tbl[5] * Math.Pow(T, 2));
            double OMEGA = this.__angle(tbl[6] + tbl[7] * T + tbl[8] * Math.Pow(T, 2));
            double i = tbl[9] + tbl[10] * T;
            double e = this.__angle(tbl[11] + tbl[12] * T);
            double a = tbl[13] + tbl[14] * T;
            if (planet == Planets.Uranus)
            {
                double n = 255.65443 / Math.Pow(tbl[13], 1.5);
                a = tbl[13] + tbl[14] * n * T;
            }

            double M = this.__angle(L - omega);     //平均近点離角
            double E = this.__kepler(M, e);         //離心近点離角

            double V = this.__angle(2 * rad2deg(Math.Atan(Math.Pow((1 + e) / (1 - e), 0.5) * Math.Tan(deg2rad(E / 2)))));

            double U = omega + V - OMEGA;       //黄緯引数
            double r = a * (1 - Math.Pow(e, 2)) / (1 + e * Math.Cos(deg2rad(V)));    //動径
            double b = rad2deg(Math.Asin(Math.Sin(deg2rad(i)) * Math.Sin(deg2rad(U))));  //黄緯
            double l = OMEGA + rad2deg(Math.Atan(Math.Cos(deg2rad(i)) * Math.Sin(deg2rad(U)) / Math.Cos(deg2rad(U))));
            if (Math.Cos(deg2rad(U)) < 0) l += 180;                      //黄経

            return new double[] { l, b, r };
        }

        /// <summary>
        ///     惑星の地心黄道座標を計算
        /// </summary>
        /// <param name="planet">惑星名</param>
        /// <param name="year">グレゴリオ暦年</param>
        /// <param name="month">グレゴリオ暦月</param>
        /// <param name="day">グレゴリオ暦日</param>
        /// <param name="hour">時（世界時）</param>
        /// <param name="min">分（世界時）</param>
        /// <param name="sec">秒（世界時）</param>
        /// <returns>array(黄経,黄緯,動径)／null：惑星名の間違い</returns>
        protected internal double[] zodiacEarth(Planets planet, int year, int month, int day, double hour, double min, double sec)
        {
            //日心黄道座標
            double[] items = this.zodiacSun(planet, year, month, day, hour, min, sec);
            if (items == null || items.Length != 3) return null;

            double l = items[0];        //日心黄経
            double b = items[1];        //日心黄緯
            double r = items[2];        //日心動径

            //太陽の黄経・黄緯
            double ls = this.longitude_sun(year, month, day, hour, min, sec);
            double rs = this.distance_sun(year, month, day, hour, min, sec);

            //直交座標へ変換
            double X = r * Math.Cos(deg2rad(b)) * Math.Cos(deg2rad(l)) + rs * Math.Cos(deg2rad(ls));
            double Y = r * Math.Cos(deg2rad(b)) * Math.Sin(deg2rad(l)) + rs * Math.Sin(deg2rad(ls));
            double Z = r * Math.Sin(deg2rad(b));

            //地心黄道座標変換
            double r1 = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2)); //動径
            double b1 = rad2deg(Math.Asin(Z / r1));                     //黄緯
            double l1 = rad2deg(Math.Atan(Y / X));
            if (X < 0) l1 += 180;
            l1 = this.__angle(l1);                          //黄経

            return new double[] { l1, b1, r1 };
        }

        /// <summary>
        ///     角度の正規化（angle を 0≦angle＜360 にする）
        /// </summary>
        /// <param name="angle">角度</param>
        /// <returns>角度（正規化後）</returns>
        protected internal double __angle(double angle)
        {
            double angle1;
            double angle2;
            if (angle < 0)
            {
                angle1 = angle * (-1);
                angle2 = Math.Floor(angle1 / 360.0);
                angle1 -= 360 * angle2;
                angle1 = 360 - angle1;
            }
            else
            {
                angle1 = Math.Floor(angle / 360.0);
                angle1 = angle - 360.0 * angle1;
            }
            return angle1;
        }

        /// <summary>
        ///     角度→ラジアン角度に変換する
        /// </summary>
        /// <param name="deg">角度</param>
        /// <returns>ラジアン角度</returns>
        protected internal double deg2rad(double deg)
        {
            const double rad_1 = Math.PI / 180.0;
            return deg * rad_1;
        }

        /// <summary>
        ///     ラジアン角度→通常の角度に変換する
        /// </summary>
        /// <param name="rad">ラジアン角度</param>
        /// <returns>通常の角度</returns>
        protected internal double rad2deg(double rad)
        {
            const double rad_1 = Math.PI / 180.0;
            return rad / rad_1;
        }

        /// <summary>
        ///     グレゴリオ暦→ユリウス日 変換
        /// </summary>
        /// <param name="year">グレゴリオ暦年</param>
        /// <param name="month">グレゴリオ暦月</param>
        /// <param name="day">グレゴリオ暦日</param>
        /// <param name="hour">時（世界時）</param>
        /// <param name="min">分（世界時）</param>
        /// <param name="sec">秒（世界時）</param>
        /// <returns>ユリウス日</returns>
        protected internal double Gregorian2JD(int year, int month, int day, double hour, double min, double sec)
        {
            if (month <= 2)
            {
                month += 12;
                year--;
            }

            double jd = Math.Floor(365.25 * year) - Math.Floor((double)year / 100) + Math.Floor((double)year / 400);
            jd += Math.Floor(30.59 * (month - 2)) + day + 1721088;
            jd += hour / 24.0 + min / (24.0 * 60.0) + sec / (24.0 * 60.0 * 60.0);

            return jd;
        }

        /**
         * 2000年1月1日力学時正午からの経過日数
         * @param	int year, month, day  グレゴリオ暦による年月日
         * @return	double 経過日数（日本標準時）
        */
        /// <summary>
        ///     2000年1月1日力学時正午からの経過日数
        /// </summary>
        /// <param name="year">グレゴリオ暦年</param>
        /// <param name="month">グレゴリオ暦月</param>
        /// <param name="day">グレゴリオ暦日</param>
        /// <returns>経過日数（日本標準時）</returns>
        protected internal double J2000(int year, int month, int day)
        {
            year -= 2000;
            if (month <= 2)
            {
                month += 12;
                year--;
            }

            double j2000 = 365.0 * year + 30.0 * month + day - 33.5;
            j2000 += Math.Floor(3.0 * (month + 1) / 5.0);
            j2000 += Math.Floor(year / 4.0);

            return j2000;
        }

        /// <summary>
        ///     2000.0からの経過年数
        /// </summary>
        /// <param name="year">グレゴリオ暦年</param>
        /// <param name="month">グレゴリオ暦月</param>
        /// <param name="day">グレゴリオ暦日</param>
        /// <param name="hour">時（世界時）</param>
        /// <param name="min">分（世界時）</param>
        /// <param name="sec">秒（世界時）</param>
        /// <returns>2000.0からの経過年数</returns>
        protected internal double Gregorian2JY(int year, int month, int day, double hour, double min, double sec)
        {
            double t = hour * 60.0 * 60.0;
            t += min * 60.0;
            t += sec;
            t /= 86400.0;

            //地球自転遅れ補正値(日)計算
            double rotate_rev = (57.0 + 0.8 * (year - 1990)) / 86400.0;

            //2000年1月1日力学時正午からの経過日数(日)計算
            double day_progress = this.J2000(year, month, day);

            //経過ユリウス年(日)計算
            //( 2000.0(2000年1月1日力学時正午)からの経過年数 (年) )
            return (day_progress + t + rotate_rev) / 365.25;
        }

        /// <summary>
        ///     太陽の黄経計算（視黄経）
        /// </summary>
        /// <param name="jy">2000.0からの経過年数</param>
        /// <returns>太陽の黄経（視黄経）</returns>
        private double __longitude_sun(double jy)
        {
            double th = 0.0003 * Math.Sin(deg2rad(this.__angle(329.7 + 44.43 * jy)));
            th += 0.0003 * Math.Sin(deg2rad(this.__angle(352.5 + 1079.97 * jy)));
            th += 0.0004 * Math.Sin(deg2rad(this.__angle(21.1 + 720.02 * jy)));
            th += 0.0004 * Math.Sin(deg2rad(this.__angle(157.3 + 299.30 * jy)));
            th += 0.0004 * Math.Sin(deg2rad(this.__angle(234.9 + 315.56 * jy)));
            th += 0.0005 * Math.Sin(deg2rad(this.__angle(291.2 + 22.81 * jy)));
            th += 0.0005 * Math.Sin(deg2rad(this.__angle(207.4 + 1.50 * jy)));
            th += 0.0006 * Math.Sin(deg2rad(this.__angle(29.8 + 337.18 * jy)));
            th += 0.0007 * Math.Sin(deg2rad(this.__angle(206.8 + 30.35 * jy)));
            th += 0.0007 * Math.Sin(deg2rad(this.__angle(153.3 + 90.38 * jy)));
            th += 0.0008 * Math.Sin(deg2rad(this.__angle(132.5 + 659.29 * jy)));
            th += 0.0013 * Math.Sin(deg2rad(this.__angle(81.4 + 225.18 * jy)));
            th += 0.0015 * Math.Sin(deg2rad(this.__angle(343.2 + 450.37 * jy)));
            th += 0.0018 * Math.Sin(deg2rad(this.__angle(251.3 + 0.20 * jy)));
            th += 0.0018 * Math.Sin(deg2rad(this.__angle(297.8 + 4452.67 * jy)));
            th += 0.0020 * Math.Sin(deg2rad(this.__angle(247.1 + 329.64 * jy)));
            th += 0.0048 * Math.Sin(deg2rad(this.__angle(234.95 + 19.341 * jy)));
            th += 0.0200 * Math.Sin(deg2rad(this.__angle(355.05 + 719.981 * jy)));
            th += (1.9146 - 0.00005 * jy) * Math.Sin(deg2rad(this.__angle(357.538 + 359.991 * jy)));
            th += this.__angle(280.4603 + 360.00769 * jy);

            return this.__angle(th);
        }

        /// <summary>
        ///     太陽の黄経計算（視黄経）
        /// </summary>
        /// <param name="year">グレゴリオ暦年</param>
        /// <param name="month">グレゴリオ暦月</param>
        /// <param name="day">グレゴリオ暦日</param>
        /// <param name="hour">時（世界時）</param>
        /// <param name="min">分（世界時）</param>
        /// <param name="sec">秒（世界時）</param>
        /// <returns>太陽の黄経（視黄経）</returns>
        protected internal double longitude_sun(int year, int month, int day, double hour, double min, double sec)
        {
            double jy = this.Gregorian2JY(year, month, day, hour, min, sec);

            return this.__longitude_sun(jy);
        }

        /// <summary>
        ///     太陽の距離計算
        /// </summary>
        /// <param name="jy">2000.0からの経過年数</param>
        /// <returns>太陽の黄経（視黄経）</returns>
        private double __distance_sun(double jy)
        {
            double r_sun = 0.000007 * Math.Sin(deg2rad(this.__angle(156.0 + 329.60 * jy)));
            r_sun += 0.000007 * Math.Sin(deg2rad(this.__angle(254.0 + 450.40 * jy)));
            r_sun += 0.000013 * Math.Sin(deg2rad(this.__angle(27.8 + 4452.67 * jy)));
            r_sun += 0.000030 * Math.Sin(deg2rad(this.__angle(90.0)));
            r_sun += 0.000091 * Math.Sin(deg2rad(this.__angle(265.1 + 719.98 * jy)));
            r_sun += (0.007256 - 0.0000002 * jy) * Math.Sin(deg2rad(this.__angle(267.54 + 359.991 * jy)));

            return Math.Pow(10, r_sun);
        }

        /// <summary>
        ///     太陽の距離計算
        /// </summary>
        /// <param name="year">グレゴリオ暦年</param>
        /// <param name="month">グレゴリオ暦月</param>
        /// <param name="day">グレゴリオ暦日</param>
        /// <param name="hour">時（世界時）</param>
        /// <param name="min">分（世界時）</param>
        /// <param name="sec">秒（世界時）</param>
        /// <returns>太陽の距離</returns>
        protected internal double distance_sun(int year, int month, int day, double hour, double min, double sec)
        {
            double jy = this.Gregorian2JY(year, month, day, hour, min, sec);

            return this.__distance_sun(jy);
        }

        /// <summary>
        ///     月の黄経計算（視黄経）
        /// </summary>
        /// <param name="jy">2000.0からの経過年数</param>
        /// <returns>月の黄経（視黄経）</returns>
        private double __longitude_moon(double jy)
        {
            double am = 0.0006 * Math.Sin(deg2rad(this.__angle(54.0 + 19.3 * jy)));
            am += 0.0006 * Math.Sin(deg2rad(this.__angle(71.0 + 0.2 * jy)));
            am += 0.0020 * Math.Sin(deg2rad(this.__angle(55.0 + 19.34 * jy)));
            am += 0.0040 * Math.Sin(deg2rad(this.__angle(119.5 + 1.33 * jy)));
            double rm_moon = 0.0003 * Math.Sin(deg2rad(this.__angle(280.0 + 23221.3 * jy)));
            rm_moon += 0.0003 * Math.Sin(deg2rad(this.__angle(161.0 + 40.7 * jy)));
            rm_moon += 0.0003 * Math.Sin(deg2rad(this.__angle(311.0 + 5492.0 * jy)));
            rm_moon += 0.0003 * Math.Sin(deg2rad(this.__angle(147.0 + 18089.3 * jy)));
            rm_moon += 0.0003 * Math.Sin(deg2rad(this.__angle(66.0 + 3494.7 * jy)));
            rm_moon += 0.0003 * Math.Sin(deg2rad(this.__angle(83.0 + 3814.0 * jy)));
            rm_moon += 0.0004 * Math.Sin(deg2rad(this.__angle(20.0 + 720.0 * jy)));
            rm_moon += 0.0004 * Math.Sin(deg2rad(this.__angle(71.0 + 9584.7 * jy)));
            rm_moon += 0.0004 * Math.Sin(deg2rad(this.__angle(278.0 + 120.1 * jy)));
            rm_moon += 0.0004 * Math.Sin(deg2rad(this.__angle(313.0 + 398.7 * jy)));
            rm_moon += 0.0005 * Math.Sin(deg2rad(this.__angle(332.0 + 5091.3 * jy)));
            rm_moon += 0.0005 * Math.Sin(deg2rad(this.__angle(114.0 + 17450.7 * jy)));
            rm_moon += 0.0005 * Math.Sin(deg2rad(this.__angle(181.0 + 19088.0 * jy)));
            rm_moon += 0.0005 * Math.Sin(deg2rad(this.__angle(247.0 + 22582.7 * jy)));
            rm_moon += 0.0006 * Math.Sin(deg2rad(this.__angle(128.0 + 1118.7 * jy)));
            rm_moon += 0.0007 * Math.Sin(deg2rad(this.__angle(216.0 + 278.6 * jy)));
            rm_moon += 0.0007 * Math.Sin(deg2rad(this.__angle(275.0 + 4853.3 * jy)));
            rm_moon += 0.0007 * Math.Sin(deg2rad(this.__angle(140.0 + 4052.0 * jy)));
            rm_moon += 0.0008 * Math.Sin(deg2rad(this.__angle(204.0 + 7906.7 * jy)));
            rm_moon += 0.0008 * Math.Sin(deg2rad(this.__angle(188.0 + 14037.3 * jy)));
            rm_moon += 0.0009 * Math.Sin(deg2rad(this.__angle(218.0 + 8586.0 * jy)));
            rm_moon += 0.0011 * Math.Sin(deg2rad(this.__angle(276.5 + 19208.02 * jy)));
            rm_moon += 0.0012 * Math.Sin(deg2rad(this.__angle(339.0 + 12678.71 * jy)));
            rm_moon += 0.0016 * Math.Sin(deg2rad(this.__angle(242.2 + 18569.38 * jy)));
            rm_moon += 0.0018 * Math.Sin(deg2rad(this.__angle(4.1 + 4013.29 * jy)));
            rm_moon += 0.0020 * Math.Sin(deg2rad(this.__angle(55.0 + 19.34 * jy)));
            rm_moon += 0.0021 * Math.Sin(deg2rad(this.__angle(105.6 + 3413.37 * jy)));
            rm_moon += 0.0021 * Math.Sin(deg2rad(this.__angle(175.1 + 719.98 * jy)));
            rm_moon += 0.0021 * Math.Sin(deg2rad(this.__angle(87.5 + 9903.97 * jy)));
            rm_moon += 0.0022 * Math.Sin(deg2rad(this.__angle(240.6 + 8185.36 * jy)));
            rm_moon += 0.0024 * Math.Sin(deg2rad(this.__angle(252.8 + 9224.66 * jy)));
            rm_moon += 0.0024 * Math.Sin(deg2rad(this.__angle(211.9 + 988.63 * jy)));
            rm_moon += 0.0026 * Math.Sin(deg2rad(this.__angle(107.2 + 13797.39 * jy)));
            rm_moon += 0.0027 * Math.Sin(deg2rad(this.__angle(272.5 + 9183.99 * jy)));
            rm_moon += 0.0037 * Math.Sin(deg2rad(this.__angle(349.1 + 5410.62 * jy)));
            rm_moon += 0.0039 * Math.Sin(deg2rad(this.__angle(111.3 + 17810.68 * jy)));
            rm_moon += 0.0040 * Math.Sin(deg2rad(this.__angle(119.5 + 1.33 * jy)));
            rm_moon += 0.0040 * Math.Sin(deg2rad(this.__angle(145.6 + 18449.32 * jy)));
            rm_moon += 0.0040 * Math.Sin(deg2rad(this.__angle(13.2 + 13317.34 * jy)));
            rm_moon += 0.0048 * Math.Sin(deg2rad(this.__angle(235.0 + 19.34 * jy)));
            rm_moon += 0.0050 * Math.Sin(deg2rad(this.__angle(295.4 + 4812.66 * jy)));
            rm_moon += 0.0052 * Math.Sin(deg2rad(this.__angle(197.2 + 319.32 * jy)));
            rm_moon += 0.0068 * Math.Sin(deg2rad(this.__angle(53.2 + 9265.33 * jy)));
            rm_moon += 0.0079 * Math.Sin(deg2rad(this.__angle(278.2 + 4493.34 * jy)));
            rm_moon += 0.0085 * Math.Sin(deg2rad(this.__angle(201.5 + 8266.71 * jy)));
            rm_moon += 0.0100 * Math.Sin(deg2rad(this.__angle(44.89 + 14315.966 * jy)));
            rm_moon += 0.0107 * Math.Sin(deg2rad(this.__angle(336.44 + 13038.696 * jy)));
            rm_moon += 0.0110 * Math.Sin(deg2rad(this.__angle(231.59 + 4892.052 * jy)));
            rm_moon += 0.0125 * Math.Sin(deg2rad(this.__angle(141.51 + 14436.029 * jy)));
            rm_moon += 0.0153 * Math.Sin(deg2rad(this.__angle(130.84 + 758.698 * jy)));
            rm_moon += 0.0305 * Math.Sin(deg2rad(this.__angle(312.49 + 5131.979 * jy)));
            rm_moon += 0.0348 * Math.Sin(deg2rad(this.__angle(117.84 + 4452.671 * jy)));
            rm_moon += 0.0410 * Math.Sin(deg2rad(this.__angle(137.43 + 4411.998 * jy)));
            rm_moon += 0.0459 * Math.Sin(deg2rad(this.__angle(238.18 + 8545.352 * jy)));
            rm_moon += 0.0533 * Math.Sin(deg2rad(this.__angle(10.66 + 13677.331 * jy)));
            rm_moon += 0.0572 * Math.Sin(deg2rad(this.__angle(103.21 + 3773.363 * jy)));
            rm_moon += 0.0588 * Math.Sin(deg2rad(this.__angle(214.22 + 638.635 * jy)));
            rm_moon += 0.1143 * Math.Sin(deg2rad(this.__angle(6.546 + 9664.0404 * jy)));
            rm_moon += 0.1856 * Math.Sin(deg2rad(this.__angle(177.525 + 359.9905 * jy)));
            rm_moon += 0.2136 * Math.Sin(deg2rad(this.__angle(269.926 + 9543.9773 * jy)));
            rm_moon += 0.6583 * Math.Sin(deg2rad(this.__angle(235.700 + 8905.3422 * jy)));
            rm_moon += 1.2740 * Math.Sin(deg2rad(this.__angle(100.738 + 4133.3536 * jy)));
            rm_moon += 6.2887 * Math.Sin(deg2rad(this.__angle(134.961 + 4771.9886 * jy + am)));

            return rm_moon + this.__angle(218.3161 + 4812.67881 * jy);
        }

        /**
         * 月の黄経計算（視黄経）
         * @param	int year, month, day  グレゴリオ暦による年月日
         * @param	double hour, min, sec 時分秒（世界時）
         * @return	double 月の黄経（視黄経）
        */
        /// <summary>
        ///     月の黄経計算（視黄経）
        /// </summary>
        /// <param name="year">グレゴリオ暦年</param>
        /// <param name="month">グレゴリオ暦月</param>
        /// <param name="day">グレゴリオ暦日</param>
        /// <param name="hour">時（世界時）</param>
        /// <param name="min">分（世界時）</param>
        /// <param name="sec">秒（世界時）</param>
        /// <returns>月の黄経（視黄経）</returns>
        protected internal double longitude_moon(int year, int month, int day, double hour, double min, double sec)
        {
            double jy = this.Gregorian2JY(year, month, day, hour, min, sec);

            return this.__longitude_moon(jy);
        }
    }
}

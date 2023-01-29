using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarViewerMaui.StarViewer
{
    /// <summary>
    ///     ホロスコープ計算
    /// </summary>
    public class horoscope : Astronomy
    {
        /// <summary>
        ///     ホロスコープ情報
        /// </summary>
        public class HoroscopeInfo
        {
            private DateTime dtDateTime;
            private bool blnTropical;
            private Planets enPlanet = Planets.Sun;
            private Sign enZodiac = Sign.Aries;
            private double dblAngle = 0.0;

            /// <summary>
            ///     日時
            /// </summary>
            public DateTime DateTime
            {
                get { return dtDateTime; }
                set { dtDateTime = value; }
            }

            /// <summary>
            ///     トロピカル方式かどうか
            /// </summary>
            public bool IsTropical
            {
                get { return blnTropical; }
                set { blnTropical = value; }
            }

            /// <summary>
            ///     惑星・恒星
            /// </summary>
            public Planets Planet
            {
                get { return this.enPlanet; }
                set { this.enPlanet = value; }
            }

            /// <summary>
            ///     星座
            /// </summary>
            public Sign Sign
            {
                get { return this.enZodiac; }
                set { this.enZodiac = value; }
            }

            /// <summary>
            ///     星座内角度
            /// </summary>
            public double Angle
            {
                get { return this.dblAngle; }
                set { this.dblAngle = value; }
            }

            /// <summary>
            ///     星座の属性
            /// </summary>
            public SignElements SignElement
            {
                get
                {
                    switch ((int)this.Sign % 4)
                    {
                        case 0: return SignElements.Fire;
                        case 1: return SignElements.Grand;
                        case 2: return SignElements.Air;
                        case 3: return SignElements.Water;
                        default: return SignElements.Fire;
                    }
                }
            }

            /// <summary>
            ///     星座３区分
            /// </summary>
            public SignGroup3 SignGroup3
            {
                get
                {
                    switch ((int)this.Sign % 3)
                    {
                        case 0: return SignGroup3.C;
                        case 1: return SignGroup3.F;
                        case 2: return SignGroup3.M;
                        default: return SignGroup3.C;
                    }
                }
            }

            /// <summary>
            ///     星座２区分（陽陰）
            /// </summary>
            public SignGroup2 SignGroup2
            {
                get
                {
                    return (int)this.SignElement % 2 == 0 ? SignGroup2.Plus : SignGroup2.Minus;
                }
            }

            /// <summary>
            ///     惑星日本語
            /// </summary>
            public string PlanetString
            {
                get
                {
                    switch (this.Planet)
                    {
                        case Planets.Sun: return "太陽";
                        case Planets.Moon: return "月";
                        case Planets.Mercury: return "水星";
                        case Planets.Venus: return "金星";
                        case Planets.Mars: return "火星";
                        case Planets.Jupiter: return "木星";
                        case Planets.Saturn: return "土星";
                        case Planets.Uranus: return "天王星";
                        case Planets.Neptune: return "海王星";
                        case Planets.Pluto: return "冥王星";
                        default: return "";
                    }
                }
            }

            /// <summary>
            ///     星座日本語
            /// </summary>
            public string ZodiacString
            {
                get
                {
                    return GetSignString(this.Sign);
                }
            }

            /// <summary>
            ///     星座属性日本語
            /// </summary>
            public string SignElementString
            {
                get
                {
                    return GetSignElementString(this.SignElement);
                }
            }

            /// <summary>
            ///     星座３区分
            /// </summary>
            public string SignGroup3String
            {
                get
                {
                    return GetSignGroup3String(this.SignGroup3);
                }
            }

            /// <summary>
            ///     星座２区分
            /// </summary>
            public string SignGroup2String
            {
                get
                {
                    return GetSignGroup2String(this.SignGroup2);
                }
            }

            /// <summary>
            ///     星座日本語を取得する。
            /// </summary>
            public static string GetSignString(Sign sign)
            {
                switch (sign)
                {
                    case Sign.Aries: return "牡羊座";
                    case Sign.Taurus: return "牡牛座";
                    case Sign.Gemini: return "双子座";
                    case Sign.Cancer: return "蟹座";
                    case Sign.Leo: return "獅子座";
                    case Sign.Virgo: return "乙女座";
                    case Sign.Libra: return "天秤座";
                    case Sign.Scorpio: return "蠍座";
                    case Sign.Sagittarius: return "射手座";
                    case Sign.Capricorn: return "山羊座";
                    case Sign.Aquarius: return "水瓶座";
                    case Sign.Pisces: return "魚座";
                    default: return "";
                }
            }

            /// <summary>
            ///     星座に対応する絵文字を取得します。
            /// </summary>
            /// <param name="sign">星座</param>
            /// <returns></returns>
            public static string GetSIgnEmoticon(Sign sign)
            {
                switch (sign)
                {
                    case Sign.Aries: return "♈";
                    case Sign.Taurus: return "♉";
                    case Sign.Gemini: return "♊";
                    case Sign.Cancer: return "♋";
                    case Sign.Leo: return "♌";
                    case Sign.Virgo: return "♍";
                    case Sign.Libra: return "♎";
                    case Sign.Scorpio: return "♏";
                    case Sign.Sagittarius: return "♐";
                    case Sign.Capricorn: return "♑";
                    case Sign.Aquarius: return "♒";
                    case Sign.Pisces: return "♓";
                    default: return "";
                }
            }

            /// <summary>
            ///     星座属性日本語を取得する。
            /// </summary>
            public static string GetSignElementString(SignElements element)
            {
                switch (element)
                {
                    case SignElements.Fire: return "火";
                    case SignElements.Grand: return "土";
                    case SignElements.Air: return "風";
                    case SignElements.Water: return "水";
                    default: return "";
                }
            }

            /// <summary>
            ///     星座３区分を取得する。
            /// </summary>
            public static string GetSignGroup3String(SignGroup3 group)
            {
                switch (group)
                {
                    case SignGroup3.C: return "活動宮";
                    case SignGroup3.F: return "不動宮";
                    case SignGroup3.M: return "柔軟宮";
                    default: return "";
                }
            }

            /// <summary>
            ///     星座２区分を取得する。
            /// </summary>
            public static string GetSignGroup2String(SignGroup2 group)
            {
                return group == SignGroup2.Plus ? "＋" : "－";
            }

            /// <summary>
            ///     度数の分数まで取得します。
            /// </summary>
            public string AngleStringAdvance2
            {
                get
                {
                    double tmp = this.Angle;
                    var degree = (int)Math.Floor(tmp);

                    tmp = (tmp - degree) * 60;
                    var minutes = (int)Math.Floor(tmp);

                    return string.Format("{0:00}'{1:00}", degree, minutes);
                }
            }

            /// <summary>
            ///     度数の秒数まで取得します。
            /// </summary>
            public string AngleStringAdvance3
            {
                get
                {
                    double tmp = this.Angle;
                    var degree = (int)Math.Floor(tmp);

                    tmp = (tmp - degree) * 60;
                    var minutes = (int)Math.Floor(tmp);

                    tmp = (tmp - minutes) * 60;
                    var seconds = (int)Math.Floor(tmp);

                    return string.Format("{0:00}'{1:00}''{2:00}", degree, minutes, seconds);
                }
            }

            /// <summary>
            ///     度数を全て取得します。
            /// </summary>
            public string AngleStringAdvanceMax
            {
                get
                {
                    double tmp = this.Angle;
                    var degree = (int)Math.Floor(tmp);

                    tmp = (tmp - degree) * 60;
                    var minutes = (int)Math.Floor(tmp);

                    tmp = (tmp - minutes) * 60;
                    var seconds = (int)Math.Floor(tmp);

                    tmp = (tmp - seconds) * 1000;
                    var last = (int)Math.Floor(tmp);

                    return string.Format("{0:00}'{1:00}''{2:00}.{3:000}", degree, minutes, seconds, last);
                }
            }
        }

        /// <summary>
        ///     ホロスコープ情報
        /// </summary>
        public class HoroscopeInfos
        {
            private DateTime dtDateTime;
            private bool blnTropical;
            private Dictionary<Planets, HoroscopeInfo> diData = new Dictionary<Planets, HoroscopeInfo>();

            /// <summary>
            ///     日時
            /// </summary>
            public DateTime DateTime
            {
                get { return dtDateTime; }
                set { dtDateTime = value; }
            }

            /// <summary>
            ///     トロピカル方式かどうか
            /// </summary>
            public bool IsTropical
            {
                get { return blnTropical; }
                set { blnTropical = value;}
            }

            /// <summary>
            ///     ホロスコープ情報
            /// </summary>
            public Dictionary<Planets, HoroscopeInfo> Horoscopes
            {
                get { return this.diData; }
            }

            /// <summary>
            ///     エレメントの数を取得する
            /// </summary>
            /// <returns>Key: エレメント, Value: 数</returns>
            public Dictionary<SignElements, int> GetElementCount()
            {
                Dictionary<SignElements, int> diRet = new Dictionary<SignElements, int>();

                foreach (Planets planets in this.diData.Keys)
                {
                    SignElements element = this.diData[planets].SignElement;
                    if (!diRet.ContainsKey(element))
                    {
                        diRet.Add(element, 0);
                    }
                    diRet[element]++;
                }

                return diRet;
            }

            /// <summary>
            ///     3区分の数を取得する。
            /// </summary>
            /// <returns>Key: 3区分, Value: 数</returns>
            public Dictionary<SignGroup3, int> GetGroup3Count()
            {
                Dictionary<SignGroup3, int> diRet = new Dictionary<SignGroup3, int>();

                foreach (Planets planets in this.diData.Keys)
                {
                    SignGroup3 group = this.diData[planets].SignGroup3;
                    if (!diRet.ContainsKey(group))
                    {
                        diRet.Add(group, 0);
                    }
                    diRet[group]++;
                }

                return diRet;
            }

            /// <summary>
            ///     2区分の数を取得する。
            /// </summary>
            /// <returns>Key: 2区分, Value: 数</returns>
            public Dictionary<SignGroup2, int> GetGroup2Count()
            {
                Dictionary<SignGroup2, int> diRet = new Dictionary<SignGroup2, int>();

                foreach (Planets planets in this.diData.Keys)
                {
                    SignGroup2 group = this.diData[planets].SignGroup2;
                    if (!diRet.ContainsKey(group))
                    {
                        diRet.Add(group, 0);
                    }
                    diRet[group]++;
                }

                return diRet;
            }

            /// <summary>
            ///     星座の数を取得する。
            /// </summary>
            /// <returns>Key: 星座, Value: 数</returns>
            public Dictionary<Sign, int> GetSignCount()
            {
                Dictionary<Sign, int> diRet = new Dictionary<Sign, int>();

                foreach (Planets planets in this.diData.Keys)
                {
                    Sign sign = this.diData[planets].Sign;
                    if (!diRet.ContainsKey(sign))
                    {
                        diRet.Add(sign, 0);
                    }
                    diRet[sign]++;
                }

                return diRet;
            }

            /// <summary>
            ///     エレメントの数を取得する。
            /// </summary>
            /// <param name="element">エレメント</param>
            /// <returns>数</returns>
            public int GetElementCount(SignElements element)
            {
                Dictionary<SignElements, int> diGroup = this.GetElementCount();
                if (!diGroup.ContainsKey(element)) return 0;
                return diGroup[element];
            }

            /// <summary>
            ///     3区分の数を取得する。
            /// </summary>
            /// <param name="group">3区分</param>
            /// <returns>数</returns>
            public int GetGroup3Count(SignGroup3 group)
            {
                Dictionary<SignGroup3, int> diGroup = this.GetGroup3Count();
                if (!diGroup.ContainsKey(group)) return 0;
                return diGroup[group];
            }

            /// <summary>
            ///     2区分の数を取得する。
            /// </summary>
            /// <param name="group">2区分</param>
            /// <returns>数</returns>
            public int GetGroup2Count(SignGroup2 group)
            {
                Dictionary<SignGroup2, int> diGroup = this.GetGroup2Count();
                if (!diGroup.ContainsKey(group)) return 0;
                return diGroup[group];
            }

            /// <summary>
            ///     星座の数を取得する。
            /// </summary>
            /// <param name="sign">星座</param>
            /// <returns>数</returns>
            public int GetSignCount(Sign sign)
            {
                Dictionary<Sign, int> diGroup = this.GetSignCount();
                if (!diGroup.ContainsKey(sign)) return 0;
                return diGroup[sign];
            }

            /// <summary>
            ///     オーバーロードしている星座を取得する。
            /// </summary>
            /// <returns></returns>
            public List<Sign> GetOverloadSign()
            {
                List<Sign> signs = new List<Sign>();

                Dictionary<Sign, int> diGroup = this.GetSignCount();

                foreach(Sign sign in diGroup.Keys)
                {
                    if (diGroup[sign] < 3) continue;    //同じ星座に３個以上星があった場合はオーバーロードとする
                    signs.Add(sign);
                }

                return signs;
            }

            /// <summary>
            ///     指定した星座がオーバーロードしているかどうか取得する。
            /// </summary>
            /// <param name="sign">星座</param>
            /// <returns>オーバーロードしているかどうか</returns>
            public bool IsOverloadSign(Sign sign)
            {
                return this.GetOverloadSign().Contains(sign);
            }
            
            /// <summary>
            ///     次の星座に行く日を取得します。
            /// </summary>
            /// <returns></returns>
            public HoroscopeInfos GetNextSignInfo()
            {
                HoroscopeInfos clsRet = new HoroscopeInfos();
                horoscope horoscope = new horoscope();

                clsRet.DateTime = this.DateTime;
                clsRet.IsTropical = this.IsTropical;

                foreach (Planets planets in this.diData.Keys)
                {
                    DateTime dt = this.dtDateTime;
                    int intAddition = 1;    //1: 日, 2: 時間, 3: 分, 4: 秒
                    while (true)
                    {
                        HoroscopeInfos clsGet = horoscope.GetHoroscope(dt, this.blnTropical);
                        bool blnEnd = false;
                        if (clsGet.diData[planets].Sign != this.diData[planets].Sign)
                        {
                            blnEnd = true;
                        }

                        if (blnEnd)
                        {
                            //星座移動の境界線がわかったら、１日ではなく１時間、一時間ではなく一分・・・と細かく探索していく。
                            switch (intAddition)
                            {
                                case 1: dt = dt.AddDays(-1); break;
                                case 2: dt = dt.AddHours(-1); break;
                                case 3: dt = dt.AddMinutes(-1); break;
                            }
                            if (intAddition == 4)
                            {
                                //正確な星座移動タイミングが掴めた
                                clsRet.diData.Add(planets, clsGet.diData[planets]);
                                break;
                            }
                            intAddition++;
                        }
                        switch (intAddition)
                        {
                            case 1: dt = dt.AddDays(1); break;
                            case 2: dt = dt.AddHours(1); break;
                            case 3: dt = dt.AddMinutes(1); break;
                            case 4: dt = dt.AddSeconds(1); break;
                        }
                    }
                }

                return clsRet;
            }

            /// <summary>
            ///     星が逆行しているかどうか
            /// </summary>
            /// <returns></returns>
            public bool IsReverse(Planets planets)
            {
                horoscope horoscope = new horoscope();
                HoroscopeInfos clsGet = horoscope.GetHoroscope(this.DateTime.AddSeconds(1), this.blnTropical);
                return this.diData[planets].Angle > clsGet.diData[planets].Angle;
            }
        }

        /// <summary>
        ///     ホロスコープを作成する
        /// </summary>
        /// <param name="dt">日時</param>
        /// <returns>ホロスコープ情報</returns>
        public HoroscopeInfos GetHoroscope(DateTime dt)
        {
            return this.GetHoroscope(dt, true);
        }

        /// <summary>
        ///     ホロスコープを作成する
        /// </summary>
        /// <param name="dt">日時</param>
        /// <param name="blnTropical">トロピカル方式かどうか</param>
        /// <returns>ホロスコープ情報</returns>
        public HoroscopeInfos GetHoroscope(DateTime dt, bool blnTropical)
        {
            DateTime dtJP = dt.AddHours(-TDIFF);
            HoroscopeInfos clsRet = new HoroscopeInfos();
            clsRet.DateTime = dt;
            clsRet.IsTropical = blnTropical;

            //オブジェクト生成
            Astronomy pas = new Astronomy();

            List<Planets> lstPlanets = new List<Planets>();
            foreach (Planets planets in Enum.GetValues(typeof(Planets)))
            {
                lstPlanets.Add(planets);
            }

            //惑星ごとにループする
            for (int i = 0; i < 10; i++)
            {
                Planets planet = lstPlanets[i];
                double l;
                double[] items;
                //太陽
                if (i == 0)
                {
                    l = pas.longitude_sun(dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second);
                    //月
                }
                else if (i == 1)
                {
                    l = pas.longitude_moon(dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second);
                    //惑星
                }
                else
                {
                    items = pas.zodiacEarth(planet, dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second);
                    l = items[0];
                }

                if (!blnTropical)
                {
                    //サイデリアル方式では２４度後ろに傾く（余りを求めるとき負にならないように360度を足す）
                    l += 360.0 - 24.0;
                }
                l = l % 360.0;
                int intSignID = (int)(l / (360.0 / 12.0));
                HoroscopeInfo info = new HoroscopeInfo();
                info.DateTime = dt;
                info.IsTropical = blnTropical;
                info.Planet = planet;
                info.Sign = (Sign)intSignID;
                info.Angle = l % (360.0 / 12.0);
                clsRet.Horoscopes.Add(info.Planet, info);
            }

            return clsRet;
        }
    }
}

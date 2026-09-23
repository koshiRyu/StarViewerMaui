using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AASharp;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Graphics;

namespace StarViewerMaui.StarViewer
{
    /// <summary>
    ///     ホロスコープ計算
    /// </summary>
    public class Horoscope
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
            ///     全体の角度
            /// </summary>
            public double AngleFull
            {
                get { return (double)this.Sign * 30.0 + this.Angle; }
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

            public Color SignElementColor
            {
                get
                {
                    switch (this.SignElement)
                    {
                        case SignElements.Fire: return Color.FromHsv(0, 255, 32);
                        case SignElements.Grand: return Color.FromHsv(64 - 16, 255, 32);
                        case SignElements.Air: return Color.FromHsv(64 + 16, 255, 32);
                        case SignElements.Water: return Color.FromHsv(180 + 32, 255, 32);
                        default: return Color.FromHsv(0, 0, 255);
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
            ///     天体マークを取得する。
            /// </summary>
            /// <param name="planets"></param>
            /// <returns></returns>
            public static string GetPlanetsStringMark(Planets planets)
            {
                switch (planets)
                {
                    case Planets.Sun: return "☉";
                    case Planets.Moon: return "☽";
                    case Planets.Mercury: return "☿";
                    case Planets.Venus: return "♀";
                    case Planets.Mars: return "♂";
                    case Planets.Jupiter: return "♃";
                    case Planets.Saturn: return "♄";
                    case Planets.Uranus: return "♅";
                    case Planets.Neptune: return "♆";
                    case Planets.Pluto: return "♇";
                    default: return "";
                }
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
                Horoscope horoscope = new Horoscope();

                clsRet.DateTime = this.DateTime;
                clsRet.IsTropical = this.IsTropical;

                foreach (Planets planets in this.diData.Keys)
                {
                    DateTime dt = this.dtDateTime;
                    int intAddition = 3;    //1: 年, 2: 月, 3: 日, 4: 時間, 5: 10分、6:分
                    switch (planets)
                    {                           //星座変更間隔
                        case Planets.Sun:       //太陽：１か月
                            intAddition = 3;
                            break;
                        case Planets.Moon:      //月：3日
                        case Planets.Mercury:   //金星：十数日
                        case Planets.Venus:     //金星：十数日
                            intAddition = 3;
                            break;
                        case Planets.Mars:      //火星：１か月少し
                            intAddition = 3;
                            break;
                        case Planets.Jupiter:   //木星：1年
                            intAddition = 2;
                            break;
                        case Planets.Saturn:    //土星：3年
                        case Planets.Uranus:    //天王星：7年
                        case Planets.Neptune:   //海王星：12年
                        case Planets.Pluto:     //冥王星：12～32年
                            intAddition = 2;
                            break;
                    }
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
                                case 1: dt = dt.AddYears(-1); break;
                                case 2: dt = dt.AddMonths(-1); break;
                                case 3: dt = dt.AddDays(-1); break;
                                case 4: dt = dt.AddHours(-1); break;
                                case 5: dt = dt.AddMinutes(-10); break;
                            }
                            if (intAddition == 6)
                            {
                                //正確な星座移動タイミングが掴めた
                                clsRet.diData.Add(planets, clsGet.diData[planets]);
                                break;
                            }
                            intAddition++;
                        }
                        switch (intAddition)
                        {
                            case 1: dt = dt.AddYears(1); break;
                            case 2: dt = dt.AddMonths(1); break;
                            case 3: dt = dt.AddDays(1); break;
                            case 4: dt = dt.AddHours(1); break;
                            case 5: dt = dt.AddMinutes(10); break;
                            case 6: dt = dt.AddMinutes(1); break;
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
                Horoscope horoscope = new Horoscope();
                HoroscopeInfos clsGet = horoscope.GetHoroscope(this.DateTime.AddSeconds(1), this.blnTropical);
                return this.diData[planets].Angle > clsGet.diData[planets].Angle;
            }
        }

        /// <summary>
        ///     アスペクト情報
        /// </summary>
        public class AspectInfo
        {
            /// <summary>
            ///     日時
            /// </summary>
            public DateTime DateTime { get; set; }

            /// <summary>
            ///     アスペクト
            /// </summary>
            public Aspects Aspects { get; set; }

            /// <summary>
            ///     天体（ホロスコープ情報）
            /// </summary>
            public List<HoroscopeInfo> Planets { get; set; } = new List<HoroscopeInfo>();
        }

        /// <summary>
        ///     グループアスペクト情報
        /// </summary>
        public class GroupAspectInfo
        {
            /// <summary>
            ///     日時
            /// </summary>
            public DateTime DateTime { get; set; }

            /// <summary>
            ///     グループアスペクト
            /// </summary>
            public GroupAspects GroupAspects { get; set; }

            /// <summary>
            ///     天体（ホロスコープ情報）
            /// </summary>
            public List<HoroscopeInfo> Planets { get; set; } = new List<HoroscopeInfo>();

            /// <summary>
            ///     グループアスペクトを文字列で取得する。
            /// </summary>
            /// <param name="aspects"></param>
            /// <returns></returns>
            public static string GetGroupAspectString(GroupAspects aspects)
            {
                switch (aspects)
                {
                    case GroupAspects.GrandTrine: return "グランドトライン";
                    case GroupAspects.GrandCross: return "グランドクロス";
                    case GroupAspects.TSquare: return "Ｔスクエア";
                    default: return "";
                }
            }
            /// <summary>
            ///     グループアスペクトを文字列で取得する。（マーク）
            /// </summary>
            /// <param name="aspects"></param>
            /// <returns></returns>
            public static string GetGroupAspectStringMark(GroupAspects aspects)
            {
                switch (aspects)
                {
                    case GroupAspects.GrandTrine: return "△";
                    case GroupAspects.GrandCross: return "◇";
                    case GroupAspects.TSquare: return "◢";
                    default: return "";
                }
            }
        }

        /// <summary>
        ///     アスペクト情報
        /// </summary>
        public class AspectInfos
        {
            /// <summary>
            ///     日時
            /// </summary>
            public DateTime DateTime { get; set; }

            /// <summary>
            ///     トロピカル方式かどうか
            /// </summary>
            public bool IsTropical { get; set; }

            /// <summary>
            ///     アスペクト情報（Key: 天体（,で連結）, Value: アスペクト情報）
            /// </summary>
            public Dictionary<string, AspectInfo> AspectInfo { get; set; } = new Dictionary<string, AspectInfo>();

            /// <summary>
            ///     アスペクト情報を取得する。
            /// </summary>
            /// <returns></returns>
            public void LoadAspectInfo()
            {
                Horoscope horoscope = new Horoscope();
                HoroscopeInfos clsGet = horoscope.GetHoroscope(this.DateTime, this.IsTropical);
                List<Planets> lstPlanets = new List<Planets>(new Planets[] { Planets.Sun, Planets.Moon, Planets.Mercury, Planets.Venus, Planets.Mars, Planets.Jupiter, Planets.Saturn, Planets.Uranus, Planets.Neptune, Planets.Pluto });
                this.AspectInfo.Clear();
                foreach (Planets planets1 in lstPlanets)
                {
                    foreach(Planets planets2 in lstPlanets)
                    {
                        if (planets1 == planets2) continue;

                        string strKey = planets1 + "," + planets2;
                        bool blnMatch = false;
                        Aspects asp = Aspects.Conjunction;
                        for (int iLoop1 = 1; iLoop1 <= 2; iLoop1++)
                        {
                            //引いた差が正になるとPlanets2の方が後に来る
                            double angleBase = clsGet.Horoscopes[planets2].AngleFull - clsGet.Horoscopes[planets1].AngleFull;
                            switch (iLoop1)
                            {
                                case 2: angleBase = (clsGet.Horoscopes[planets2].AngleFull + 360) - clsGet.Horoscopes[planets1].AngleFull; break;
                            }
                            switch (angleBase)
                            {
                                case double angle when angle >= 0 && angle <= 8:
                                {
                                    blnMatch = true;
                                    asp = Aspects.Conjunction;
                                    break;
                                }
                                case double angle when angle >= 60 - 6 && angle <= 60 + 6:
                                {
                                    blnMatch = true;
                                    asp = Aspects.Sextile;
                                    break;
                                }
                                case double angle when angle >= 90 - 8 && angle <= 90 + 8:
                                {
                                    blnMatch = true;
                                    asp = Aspects.Square;
                                    break;
                                }
                                case double angle when angle >= 120 - 8 && angle <= 120 + 8:
                                {
                                    blnMatch = true;
                                    asp = Aspects.Trine;
                                    break;
                                }
                                case double angle when angle >= 180 - 8 && angle <= 180 + 8:
                                {
                                    blnMatch = true;
                                    asp = Aspects.Opposition;
                                    break;
                                }
                            }
                        }
                        if (blnMatch)
                        {
                            if (!this.AspectInfo.ContainsKey(strKey))
                            {
                                AspectInfo aspectInfo = new AspectInfo();
                                aspectInfo.DateTime = this.DateTime;
                                aspectInfo.Planets.Add(clsGet.Horoscopes[planets1]);
                                aspectInfo.Planets.Add(clsGet.Horoscopes[planets2]);
                                aspectInfo.Aspects = asp;
                                this.AspectInfo.Add(strKey, aspectInfo);
                            }
                        }
                    }
                }
            }
            
            /// <summary>
            ///     アスペクト情報を取得する。
            /// </summary>
            /// <param name="dtStart">開始日</param>
            /// <param name="dtEnd">終了日</param>
            /// <param name="blnTropical">トロピカル方式かどうか</param>
            /// <returns>Key: 日時, Value: アスペクト情報</returns>
            public static Dictionary<DateTime, AspectInfos> GetAspectInfos(DateTime dtStart, DateTime dtEnd, bool blnTropical)
            {
                Dictionary<DateTime, AspectInfos> diRet = new Dictionary<DateTime, AspectInfos>();

                if (dtStart > dtEnd)
                {
                    DateTime dtTmp = dtStart;
                    dtStart = dtEnd;
                    dtEnd = dtTmp;
                }

                for (DateTime dt = dtStart; dt <= dtEnd; dt = dt.AddHours(6))
                {
                    AspectInfos clsInfo = new AspectInfos();
                    clsInfo.DateTime = dt;
                    clsInfo.IsTropical = blnTropical;
                    clsInfo.LoadAspectInfo();
                    if (clsInfo.AspectInfo.Count == 0) continue;
                    diRet.Add(dt, clsInfo);
                }

                return diRet;
            }
        }

        /// <summary>
        ///     グループアスペクト情報
        /// </summary>
        public class GroupAspectInfos
        {
            /// <summary>
            ///     日時
            /// </summary>
            public DateTime DateTime { get; set; }

            /// <summary>
            ///     トロピカル方式かどうか
            /// </summary>
            public bool IsTropical { get; set; }

            /// <summary>
            ///     アスペクト情報（Key: 天体（,で連結）, Value: アスペクト情報）
            /// </summary>
            public Dictionary<string, GroupAspectInfo> GroupAspectInfo { get; set; } = new Dictionary<string, GroupAspectInfo>();

            /// <summary>
            ///     グループアスペクト情報を読み込む。
            /// </summary>
            public void LoadGroupAspectInfo()
            {
                this.GroupAspectInfo.Clear();
                AspectInfos clsAspect = new AspectInfos();
                clsAspect.DateTime = this.DateTime;
                clsAspect.IsTropical = this.IsTropical;
                clsAspect.LoadAspectInfo();
                List<Planets> lstPlanets = new List<Planets>(new Planets[] { Planets.Sun, Planets.Moon, Planets.Mercury, Planets.Venus, Planets.Mars, Planets.Jupiter, Planets.Saturn, Planets.Uranus, Planets.Neptune, Planets.Pluto });
                List<Sign> lstSigns = Enum.GetValues<Sign>().ToList();

                //グランドトラインを探す
                foreach (string strKey in clsAspect.AspectInfo.Keys)
                {
                    List<string> lstStars = new List<string>();
                    string[] strSplit = strKey.Split(',');
                    if (clsAspect.AspectInfo[strKey].Aspects == Aspects.Trine)
                    {
                        //星A・Bが合致した
                        bool blnMatch1 = false;
                        bool blnMatch2 = false;
                        string strKey2Bk = "";
                        string strKey3Bk = "";
                        lstStars.Add(strSplit[0]);
                        lstStars.Add(strSplit[1]);

                        //星Bが合致するもう１つのアスペクトを調べる
                        foreach (string strKey2 in clsAspect.AspectInfo.Keys)
                        {
                            if (strKey == strKey2) continue;

                            string[] strSplit2 = strKey2.Split(',');
                            if (clsAspect.AspectInfo[strKey2].Aspects == Aspects.Trine)
                            {
                                if (lstStars[1] == strSplit2[0])
                                {
                                    lstStars.Add(strSplit2[1]);
                                    blnMatch1 = true;
                                    strKey2Bk = strKey2;
                                    break;
                                }
                            }
                        }
                        if (!blnMatch1) continue;

                        //星C・Aが合致するかどうか調べる。
                        foreach (string strKey3 in clsAspect.AspectInfo.Keys)
                        {
                            if (strKey == strKey3) continue;
                            if (strKey2Bk == strKey3) continue;

                            string[] strSplit3 = strKey3.Split(',');
                            if (clsAspect.AspectInfo[strKey3].Aspects == Aspects.Trine)
                            {
                                if (lstStars[2] == strSplit3[0] && lstStars[0] == strSplit3[1])
                                {
                                    blnMatch2 = true;
                                    strKey3Bk = strKey3;
                                    break;
                                }
                            }
                        }

                        if (blnMatch2)
                        {
                            //アスペクト発見
                            //キーの同一性を保つため、天体をソートする。
                            //星C・星A・星Bの順番に並んでいても、星B・星A・星Cの順番に並んでいても、星A・星B・星Cの順番に並べ替える。
                            lstStars.Sort(delegate (string item1, string item2)
                            {
                                Planets pitem1 = Enum.Parse<Planets>(item1);
                                Planets pitem2 = Enum.Parse<Planets>(item2);
                                return lstPlanets.IndexOf(pitem1) - lstPlanets.IndexOf(pitem2);
                            });
                            string strDicKey = string.Join(",", lstStars.ToArray());
                            if (!this.GroupAspectInfo.ContainsKey(strDicKey))
                            {
                                GroupAspectInfo aspectInfo = new GroupAspectInfo();
                                aspectInfo.DateTime = this.DateTime;
                                aspectInfo.GroupAspects = GroupAspects.GrandTrine;
                                string[] strKeys = new string[] { strKey, strKey2Bk, strKey3Bk };
                                Dictionary<Planets, HoroscopeInfo> diPlanets = new Dictionary<Planets, HoroscopeInfo>();
                                List<HoroscopeInfo> lstHoroInfo = new List<HoroscopeInfo>();
                                foreach (string strKeyLoop in strKeys)
                                {
                                    foreach (HoroscopeInfo horo in clsAspect.AspectInfo[strKeyLoop].Planets)
                                    {
                                        if (!diPlanets.ContainsKey(horo.Planet))
                                        {
                                            diPlanets.Add(horo.Planet, horo);
                                            lstHoroInfo.Add(horo);
                                        }
                                    }
                                }
                                lstHoroInfo.Sort(delegate (HoroscopeInfo item1, HoroscopeInfo item2)
                                {
                                    Sign sitem1 = item1.Sign;
                                    Sign sitem2 = item2.Sign;
                                    return lstSigns.IndexOf(sitem1) - lstSigns.IndexOf(sitem2);
                                });
                                foreach (HoroscopeInfo horoInfo in lstHoroInfo)
                                {
                                    aspectInfo.Planets.Add(horoInfo);
                                }
                                this.GroupAspectInfo.Add(strDicKey, aspectInfo);
                            }
                        }
                    }
                }

                //グランドクロスを探す
                foreach (string strKey in clsAspect.AspectInfo.Keys)
                {
                    List<string> lstStars = new List<string>();
                    string[] strSplit = strKey.Split(',');
                    if (clsAspect.AspectInfo[strKey].Aspects == Aspects.Square)
                    {
                        //星A・Bが合致した
                        bool blnMatch1 = false;
                        bool blnMatch2 = false;
                        bool blnMatch3 = false;
                        string strKey2Bk = "";
                        string strKey3Bk = "";
                        string strKey4Bk = "";
                        lstStars.Add(strSplit[0]);
                        lstStars.Add(strSplit[1]);

                        //星Bが合致するもう１つのアスペクトを調べる
                        foreach (string strKey2 in clsAspect.AspectInfo.Keys)
                        {
                            if (strKey == strKey2) continue;

                            string[] strSplit2 = strKey2.Split(',');
                            if (clsAspect.AspectInfo[strKey2].Aspects == Aspects.Square)
                            {
                                if (lstStars[1] == strSplit2[0])
                                {
                                    lstStars.Add(strSplit2[1]);
                                    blnMatch1 = true;
                                    strKey2Bk = strKey2;
                                    break;
                                }
                            }
                        }
                        if (!blnMatch1) continue;

                        //星Cが合致するもう１つのアスペクトを調べる
                        foreach (string strKey3 in clsAspect.AspectInfo.Keys)
                        {
                            if (strKey == strKey3) continue;
                            if (strKey2Bk == strKey3) continue;

                            string[] strSplit3 = strKey3.Split(',');
                            if (clsAspect.AspectInfo[strKey3].Aspects == Aspects.Square)
                            {
                                if (lstStars[2] == strSplit3[0])
                                {
                                    lstStars.Add(strSplit3[1]);
                                    blnMatch2 = true;
                                    strKey3Bk = strKey3;
                                    break;
                                }
                            }
                        }
                        if (!blnMatch2) continue;

                        //星D・Aが合致するかどうか調べる。
                        foreach (string strKey4 in clsAspect.AspectInfo.Keys)
                        {
                            if (strKey == strKey4) continue;
                            if (strKey2Bk == strKey4) continue;
                            if (strKey3Bk == strKey4) continue;

                            string[] strSplit4 = strKey4.Split(',');
                            if (clsAspect.AspectInfo[strKey4].Aspects == Aspects.Square)
                            {
                                if (lstStars[3] == strSplit4[0] && lstStars[0] == strSplit4[1])
                                {
                                    blnMatch3 = true;
                                    strKey4Bk = strKey4;
                                    break;
                                }
                            }
                        }

                        if (blnMatch3)
                        {
                            //アスペクト発見
                            //キーの同一性を保つため、天体をソートする。
                            //星C・星A・星Bの順番に並んでいても、星B・星A・星Cの順番に並んでいても、星A・星B・星Cの順番に並べ替える。
                            lstStars.Sort(delegate (string item1, string item2)
                            {
                                Planets pitem1 = Enum.Parse<Planets>(item1);
                                Planets pitem2 = Enum.Parse<Planets>(item2);
                                return lstPlanets.IndexOf(pitem1) - lstPlanets.IndexOf(pitem2);
                            });
                            string strDicKey = string.Join(",", lstStars.ToArray());
                            if (!this.GroupAspectInfo.ContainsKey(strDicKey))
                            {
                                GroupAspectInfo aspectInfo = new GroupAspectInfo();
                                aspectInfo.DateTime = this.DateTime;
                                aspectInfo.GroupAspects = GroupAspects.GrandCross;
                                string[] strKeys = new string[] { strKey, strKey2Bk, strKey3Bk, strKey4Bk };
                                Dictionary<Planets, HoroscopeInfo> diPlanets = new Dictionary<Planets, HoroscopeInfo>();
                                List<HoroscopeInfo> lstHoroInfo = new List<HoroscopeInfo>();
                                foreach (string strKeyLoop in strKeys)
                                {
                                    foreach (HoroscopeInfo horo in clsAspect.AspectInfo[strKeyLoop].Planets)
                                    {
                                        if (!diPlanets.ContainsKey(horo.Planet))
                                        {
                                            diPlanets.Add(horo.Planet, horo);
                                            lstHoroInfo.Add(horo);
                                        }
                                    }
                                }
                                lstHoroInfo.Sort(delegate (HoroscopeInfo item1, HoroscopeInfo item2)
                                {
                                    Sign sitem1 = item1.Sign;
                                    Sign sitem2 = item2.Sign;
                                    return lstSigns.IndexOf(sitem1) - lstSigns.IndexOf(sitem2);
                                });
                                foreach (HoroscopeInfo horoInfo in lstHoroInfo)
                                {
                                    aspectInfo.Planets.Add(horoInfo);
                                }
                                this.GroupAspectInfo.Add(strDicKey, aspectInfo);
                            }
                        }
                    }
                }

                //Tスクエアを探す
                foreach (string strKey in clsAspect.AspectInfo.Keys)
                {
                    List<string> lstStars = new List<string>();
                    string[] strSplit = strKey.Split(',');
                    if (clsAspect.AspectInfo[strKey].Aspects == Aspects.Opposition)
                    {
                        //星A・Bが合致した
                        bool blnMatch1 = false;
                        bool blnMatch2 = false;
                        string strKey2Bk = "";
                        string strKey3Bk = "";
                        lstStars.Add(strSplit[0]);
                        lstStars.Add(strSplit[1]);

                        //星Bが合致するもう１つのアスペクトを調べる
                        foreach (string strKey2 in clsAspect.AspectInfo.Keys)
                        {
                            if (strKey == strKey2) continue;

                            string[] strSplit2 = strKey2.Split(',');
                            if (clsAspect.AspectInfo[strKey2].Aspects == Aspects.Square)
                            {
                                if (lstStars[1] == strSplit2[0])
                                {
                                    lstStars.Add(strSplit2[1]);
                                    blnMatch1 = true;
                                    strKey2Bk = strKey2;
                                    break;
                                }
                            }
                        }
                        if (!blnMatch1) continue;

                        //星C・Aが合致するかどうか調べる。
                        foreach (string strKey3 in clsAspect.AspectInfo.Keys)
                        {
                            if (strKey == strKey3) continue;
                            if (strKey2Bk == strKey3) continue;

                            string[] strSplit3 = strKey3.Split(',');
                            if (clsAspect.AspectInfo[strKey3].Aspects == Aspects.Square)
                            {
                                if (lstStars[2] == strSplit3[0] && lstStars[0] == strSplit3[1])
                                {
                                    blnMatch2 = true;
                                    strKey3Bk = strKey3;
                                    break;
                                }
                            }
                        }

                        if (blnMatch2)
                        {
                            //アスペクト発見
                            //キーの同一性を保つため、天体をソートする。
                            //星C・星A・星Bの順番に並んでいても、星B・星A・星Cの順番に並んでいても、星A・星B・星Cの順番に並べ替える。
                            lstStars.Sort(delegate (string item1, string item2)
                            {
                                Planets pitem1 = Enum.Parse<Planets>(item1);
                                Planets pitem2 = Enum.Parse<Planets>(item2);
                                return lstPlanets.IndexOf(pitem1) - lstPlanets.IndexOf(pitem2);
                            });
                            string strDicKey = string.Join(",", lstStars.ToArray());
                            if (!this.GroupAspectInfo.ContainsKey(strDicKey))
                            {
                                GroupAspectInfo aspectInfo = new GroupAspectInfo();
                                aspectInfo.DateTime = this.DateTime;
                                aspectInfo.GroupAspects = GroupAspects.TSquare;
                                string[] strKeys = new string[] { strKey, strKey2Bk, strKey3Bk };
                                Dictionary<Planets, HoroscopeInfo> diPlanets = new Dictionary<Planets, HoroscopeInfo>();
                                List<HoroscopeInfo> lstHoroInfo = new List<HoroscopeInfo>();
                                foreach (string strKeyLoop in strKeys)
                                {
                                    foreach (HoroscopeInfo horo in clsAspect.AspectInfo[strKeyLoop].Planets)
                                    {
                                        if (!diPlanets.ContainsKey(horo.Planet))
                                        {
                                            diPlanets.Add(horo.Planet, horo);
                                            lstHoroInfo.Add(horo);
                                        }
                                    }
                                }
                                lstHoroInfo.Sort(delegate (HoroscopeInfo item1, HoroscopeInfo item2)
                                {
                                    Sign sitem1 = item1.Sign;
                                    Sign sitem2 = item2.Sign;
                                    return lstSigns.IndexOf(sitem1) - lstSigns.IndexOf(sitem2);
                                });
                                foreach (HoroscopeInfo horoInfo in lstHoroInfo)
                                {
                                    aspectInfo.Planets.Add(horoInfo);
                                }
                                this.GroupAspectInfo.Add(strDicKey, aspectInfo);
                            }
                        }
                    }
                }
            }

            /// <summary>
            ///     アスペクト情報を取得する。
            /// </summary>
            /// <param name="dtStart">開始日</param>
            /// <param name="dtEnd">終了日</param>
            /// <param name="blnTropical">トロピカル方式かどうか</param>
            /// <returns>Key: 日時, Value: アスペクト情報</returns>
            public static Dictionary<DateTime, GroupAspectInfos> GetGroupAspectInfos(DateTime dtStart, DateTime dtEnd, bool blnTropical)
            {
                Dictionary<DateTime, GroupAspectInfos> diRet = new Dictionary<DateTime, GroupAspectInfos>();

                if (dtStart > dtEnd)
                {
                    DateTime dtTmp = dtStart;
                    dtStart = dtEnd;
                    dtEnd = dtTmp;
                }

                for (DateTime dt = dtStart; dt <= dtEnd; dt = dt.AddHours(6))
                {
                    GroupAspectInfos clsInfo = new GroupAspectInfos();
                    clsInfo.DateTime = dt;
                    clsInfo.IsTropical = blnTropical;
                    clsInfo.LoadGroupAspectInfo();
                    if (clsInfo.GroupAspectInfo.Count == 0) continue;
                    diRet.Add(dt, clsInfo);
                }

                return diRet;
            }
        }

        /// <summary>
        ///     グループアスペクト情報
        /// </summary>
        public class GroupAspectInfosMultiDate
        {
            /// <summary>
            ///     開始日時
            /// </summary>
            public DateTime DateTimeStart { get; set; }

            /// <summary>
            ///     終了日時
            /// </summary>
            public DateTime DateTimeEnd { get; set; }

            /// <summary>
            ///     グループアスペクト
            /// </summary>
            public GroupAspects GroupAspects { get; set; }

            /// <summary>
            ///     天体（ホロスコープ情報）
            /// </summary>
            public List<HoroscopeInfo> Planets { get; set; } = new List<HoroscopeInfo>();

            /// <summary>
            ///     アスペクトを色で表す
            /// </summary>
            /// <returns></returns>
            public Color GetGroupAspectColor()
            {
                bool blnExistPluto = this.Planets.Where(horo => horo.Planet == StarViewer.Planets.Pluto).Count() > 0;
                bool blnExistTrans = this.Planets.Where(horo => horo.Planet == StarViewer.Planets.Uranus || horo.Planet == StarViewer.Planets.Neptune || horo.Planet == StarViewer.Planets.Pluto).Count() > 0;
                if (this.GroupAspects == GroupAspects.GrandTrine)
                {
                    //吉相
                    if (blnExistPluto) return Color.FromHsv(180 + 32, 255, 40);
                    else if (blnExistTrans) return Color.FromHsv(64 + 16, 255, 40);
                    else return Color.FromHsv(64 + 16, 32, 40);
                }
                else
                {
                    //凶相
                    if (blnExistPluto) return Color.FromHsv(0, 255, 40);
                    else if (blnExistTrans) return Color.FromHsv(64, 255, 40);
                    else return Color.FromHsv(64, 32, 40);
                }
            }

            /// <summary>
            ///     アスペクト情報を取得する。
            /// </summary>
            /// <param name="dtStart">開始日</param>
            /// <param name="intItems">表示数</param>
            /// <param name="blnTropical">トロピカル方式かどうか</param>
            /// <returns>Key: 日時, Value: アスペクト情報</returns>
            public static List<GroupAspectInfosMultiDate> GetGroupAspectInfos(DateTime dtStart, int intItems, bool blnTropical)
            {
                List<GroupAspectInfosMultiDate> lstRet = new List<GroupAspectInfosMultiDate>();
                Dictionary<string, GroupAspectInfosMultiDate> diRetTemplate = new Dictionary<string, GroupAspectInfosMultiDate>();

                DateTime dt = dtStart;
                while (true)
                {
                    //表示数集計し終わって、終了日まで集計出来たら処理を終了する
                    if (lstRet.Count >= intItems && diRetTemplate.Count == 0) break;

                    GroupAspectInfos clsInfo = new GroupAspectInfos();
                    clsInfo.DateTime = dt;
                    clsInfo.IsTropical = blnTropical;
                    clsInfo.LoadGroupAspectInfo();

                    foreach (string strKeyPlanets in clsInfo.GroupAspectInfo.Keys)
                    {
                        if (!diRetTemplate.ContainsKey(strKeyPlanets))
                        {
                            if (lstRet.Count < intItems)
                            {
                                GroupAspectInfosMultiDate clsRet = new GroupAspectInfosMultiDate();
                                clsRet.DateTimeStart = dt;
                                clsRet.DateTimeEnd = dt;
                                clsRet.GroupAspects = clsInfo.GroupAspectInfo[strKeyPlanets].GroupAspects;
                                clsRet.Planets = clsInfo.GroupAspectInfo[strKeyPlanets].Planets;
                                diRetTemplate.Add(strKeyPlanets, clsRet);
                                lstRet.Add(clsRet);
                            }
                        }
                        else
                        {
                            diRetTemplate[strKeyPlanets].DateTimeEnd = dt;
                        }
                    }
                    //アスペクトが現在ループしている日時も存在するか調べる。なかったら削除して再度同じ天体がアスペクトをとったら別枠で表示する。
                    foreach (string strKeyPlanets in diRetTemplate.Keys)
                    {
                        if (!clsInfo.GroupAspectInfo.ContainsKey(strKeyPlanets))
                        {
                            diRetTemplate.Remove(strKeyPlanets);
                        }
                    }
                    dt = dt.AddHours(6);
                }

                return lstRet;
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
            DateTime dtJP = dt.AddHours(-9);
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
                double l = 0;
                switch (planet)
                {
                    case Planets.Sun: l = AASElliptical.Calculate(GetJulianDay(dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second), AASEllipticalObject.SUN, false).ApparentGeocentricLongitude; break;
                    case Planets.Moon: l = AASMoon.EclipticLongitude(GetJulianDay(dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second)); break;
                    case Planets.Mercury: l = AASElliptical.Calculate(GetJulianDay(dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second), AASEllipticalObject.MERCURY, false).ApparentGeocentricLongitude; break;
                    case Planets.Venus: l = AASElliptical.Calculate(GetJulianDay(dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second), AASEllipticalObject.VENUS, false).ApparentGeocentricLongitude; break;
                    case Planets.Mars: l = AASElliptical.Calculate(GetJulianDay(dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second), AASEllipticalObject.MARS, false).ApparentGeocentricLongitude; break;
                    case Planets.Jupiter: l = AASElliptical.Calculate(GetJulianDay(dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second), AASEllipticalObject.JUPITER, false).ApparentGeocentricLongitude; break;
                    case Planets.Saturn: l = AASElliptical.Calculate(GetJulianDay(dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second), AASEllipticalObject.SATURN, false).ApparentGeocentricLongitude; break;
                    case Planets.Uranus: l = AASElliptical.Calculate(GetJulianDay(dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second), AASEllipticalObject.URANUS, false).ApparentGeocentricLongitude; break;
                    case Planets.Neptune: l = AASElliptical.Calculate(GetJulianDay(dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second), AASEllipticalObject.NEPTUNE, false).ApparentGeocentricLongitude; break;
                    case Planets.Pluto: l = this.GetPlutoApparentGeocentricLongitude(GetJulianDay(dtJP.Year, dtJP.Month, dtJP.Day, dtJP.Hour, dtJP.Minute, dtJP.Second)); break;
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

        /// <summary>
        ///     ユリウス日を取得する
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private double GetJulianDay(int year, int month, int day, double hour, double minute, double second)
        {
            AASDate date = new AASDate(year, month, day, hour, minute, second, true);
            double dblDeltaT = AASDynamicalTime.DeltaT(date.Julian);
            return date.Julian + dblDeltaT / 86400;
        }

        /// <summary>
        ///     冥王星の地心の視黄経を求める。
        ///     （AASharpの標準の機能では若干の度数のずれがあったため、AASharpを用いながら自身で実装する。）
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private double GetPlutoApparentGeocentricLongitude(double jd)
        {
            const double AU = 149597870700.0 / 1000.0;
            AASEllipticalObjectElements elements = new AASEllipticalObjectElements();
            elements.a = 5.878639592778637E+09 / AU;
            elements.e = 2.440100688210862E-01;
            elements.i = 1.693695129099645E+01;
            elements.omega = 1.102126326826384E+02;
            elements.w = 1.135828678805157E+02;
            elements.T = 2447710.217061389238;
            AASEllipticalObjectDetails details = AASElliptical.Calculate(jd, ref elements, true);
            AAS3DCoordinate detailPluto = details.HeliocentricRectangularEcliptical;
            AAS3DCoordinate detailSun = AASSun.EclipticRectangularCoordinatesJ2000(jd, true);

            double X = detailPluto.X + detailSun.X;
            double Y = detailPluto.Y + detailSun.Y;

            //地心黄経変換
            double l1 = GetRad2Deg(Math.Atan(Y / X));
            if (X < 0) l1 += 180;
            l1 = this.GetAngle(l1);

            //HACK: 補正値として一定値を足す。ただしいずれかはなくす。
            l1 += 1.0 / 60.0 * 19.0;

            return l1;
        }

        /// <summary>
        ///     ケプラー運動方程式の解法（漸化法）
        /// </summary>
        /// <param name="l">平均近点離角</param>
        /// <param name="e">軌道離心率</param>
        /// <returns>離心近点離角</returns>
        private double GetKepler(double l, double e)
        {
            return AASKepler.Calculate(l, e);
        }

        /// <summary>
        ///     角度の正規化（angle を 0≦angle＜360 にする）
        /// </summary>
        /// <param name="angle">角度</param>
        /// <returns>角度（正規化後）</returns>
        private double GetAngle(double angle)
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
        private double GetDeg2Rad(double deg)
        {
            return AASCoordinateTransformation.DegreesToRadians(deg);
        }

        /// <summary>
        ///     ラジアン角度→通常の角度に変換する
        /// </summary>
        /// <param name="rad">ラジアン角度</param>
        /// <returns>通常の角度</returns>
        private double GetRad2Deg(double rad)
        {
            return AASCoordinateTransformation.RadiansToDegrees(rad);
        }
    }
}


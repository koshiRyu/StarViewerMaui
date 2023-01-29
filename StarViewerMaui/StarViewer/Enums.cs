using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarViewerMaui.StarViewer
{
    /// <summary>
    ///     惑星
    /// </summary>
    public enum Planets
    {
        /// <summary>太陽</summary>
        Sun,
        /// <summary>月</summary>
        Moon,
        /// <summary>水星</summary>
        Mercury,
        /// <summary>金星</summary>
        Venus,
        /// <summary>火星</summary>
        Mars,
        /// <summary>木星</summary>
        Jupiter,
        /// <summary>土星</summary>
        Saturn,
        /// <summary>天王星</summary>
        Uranus,
        /// <summary>海王星</summary>
        Neptune,
        /// <summary>冥王星</summary>
        Pluto,
        /// <summary>地球</summary>
        Earth,
    }

    /// <summary>
    ///     星座
    /// </summary>
    public enum Sign
    {
        /// <summary>牡羊座</summary>
        Aries,
        /// <summary>牡牛座</summary>
        Taurus,
        /// <summary>双子座</summary>
        Gemini,
        /// <summary>蟹座</summary>
        Cancer,
        /// <summary>獅子座</summary>
        Leo,
        /// <summary>乙女座</summary>
        Virgo,
        /// <summary>天秤座</summary>
        Libra,
        /// <summary>蠍座</summary>
        Scorpio,
        /// <summary>射手座</summary>
        Sagittarius,
        /// <summary>山羊座</summary>
        Capricorn,
        /// <summary>水瓶座</summary>
        Aquarius,
        /// <summary>魚座</summary>
        Pisces
    }

    /// <summary>
    ///     星座の属性
    /// </summary>
    public enum SignElements
    {
        /// <summary>火</summary>
        Fire,
        /// <summary>土</summary>
        Grand,
        /// <summary>風</summary>
        Air,
        /// <summary>水</summary>
        Water
    }

    /// <summary>
    ///     星座３区分
    /// </summary>
    public enum SignGroup3
    {
        /// <summary>活動宮</summary>
        C,
        /// <summary>不動宮</summary>
        F,
        /// <summary>柔軟宮</summary>
        M
    }

    /// <summary>
    ///     星座２区分
    /// </summary>
    public enum SignGroup2
    {
        /// <summary>陽</summary>
        Plus,
        /// <summary>陰</summary>
        Minus
    }
}

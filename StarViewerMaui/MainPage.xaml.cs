using System.Text.Json;
using System;
using StarViewerMaui.StarViewer;
using System.Linq;

namespace StarViewerMaui;

public partial class MainPage : ContentPage
{
    IDispatcherTimer timer;

    /// <summary>
    ///     表示アイテム
    /// </summary>
    private class DispItems
    {
        /// <summary>天体</summary>
        public Label Star { get; set; }
        /// <summary>星座名</summary>
        public Label SignName { get; set; }
        /// <summary>星座</summary>
        public Label Sign { get; set; }
        /// <summary>星が逆行しているかどうか</summary>
        public Label Reversed { get; set; }
        /// <summary>度数</summary>
        public Label Sabian { get; set; }
        /// <summary>次の星座名</summary>
        public Label NextName { get; set; }
        /// <summary>次の星座</summary>
        public Label Next { get; set; }
        /// <summary>次の星座に移動する日</summary>
        public Label NextYMD { get; set; }
    }

    public MainPage()
	{
		InitializeComponent();
	}

    private void Update()
    {
        DateTime dt = DateTime.Now;
        horoscope horo = new horoscope();
        horoscope.HoroscopeInfos horoInfos = horo.GetHoroscope(dt);
        horoscope.HoroscopeInfos horoNext = horoInfos.GetNextSignInfo();

        Dictionary<Planets, DispItems> diDispItems = this.GetDispItems();
        this.AppTitle.Text = string.Format("星巡りViewer {0}", dt);
        foreach (Planets planets in horoInfos.Horoscopes.Keys)
        {
            horoscope.HoroscopeInfo horoItem = horoInfos.Horoscopes[planets];

            string strStarName = horoItem.PlanetString;
            while (strStarName.Length < 3) strStarName = "　" + strStarName;

            DispItems clsDisp = diDispItems[planets];
            clsDisp.Star.Text = strStarName;
            clsDisp.SignName.Text = horoscope.HoroscopeInfo.GetSignString(horoItem.Sign);
            clsDisp.Sign.Text = horoscope.HoroscopeInfo.GetSIgnEmoticon(horoItem.Sign);
            clsDisp.Reversed.Text = horoInfos.IsReverse(planets) ? "R:逆行" : "";
            clsDisp.Sabian.Text = horoItem.AngleStringAdvanceMax;
            clsDisp.NextName.Text = horoscope.HoroscopeInfo.GetSignString(horoNext.Horoscopes[planets].Sign);
            clsDisp.Next.Text = horoscope.HoroscopeInfo.GetSIgnEmoticon(horoNext.Horoscopes[planets].Sign);
            clsDisp.NextYMD.Text = horoNext.Horoscopes[planets].DateTime.ToString("yyyy/MM/dd");
        }
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        this.Update();
        this.timer = Dispatcher.CreateTimer();
        this.timer.Interval = TimeSpan.FromMilliseconds(500);
        this.timer.Tick += (s, e) => Update();
        this.timer.Start();
    }

    /// <summary>
    ///     表示アイテムを取得する。
    /// </summary>
    /// <returns></returns>
    private Dictionary<Planets, DispItems> GetDispItems()
    {
        return new Dictionary<Planets, DispItems>
        {
            {Planets.Sun, new DispItems{Star=this.lbl1_star, SignName=this.lbl1_sign_name, Sign=this.lbl1_sign, Reversed=this.lbl1_reverse, Sabian=this.lbl1_sabian, NextName=this.lbl1_next_sign_name, Next=this.lbl1_next_sign, NextYMD=this.lbl1_next_ymd} },
            {Planets.Moon, new DispItems{Star=this.lbl2_star,SignName=this.lbl2_sign_name,  Sign=this.lbl2_sign, Reversed=this.lbl2_reverse, Sabian=this.lbl2_sabian, NextName=this.lbl2_next_sign_name, Next=this.lbl2_next_sign, NextYMD=this.lbl2_next_ymd} },
            {Planets.Mercury, new DispItems{Star=this.lbl3_star, SignName=this.lbl3_sign_name, Sign=this.lbl3_sign, Reversed=this.lbl3_reverse, Sabian=this.lbl3_sabian, NextName=this.lbl3_next_sign_name,Next=this.lbl3_next_sign, NextYMD=this.lbl3_next_ymd} },
            {Planets.Venus, new DispItems{Star=this.lbl4_star, SignName=this.lbl4_sign_name, Sign=this.lbl4_sign, Reversed=this.lbl4_reverse, Sabian=this.lbl4_sabian, NextName=this.lbl4_next_sign_name,Next=this.lbl4_next_sign, NextYMD=this.lbl4_next_ymd} },
            {Planets.Mars, new DispItems{Star=this.lbl5_star, SignName=this.lbl5_sign_name, Sign=this.lbl5_sign, Reversed=this.lbl5_reverse, Sabian=this.lbl5_sabian, NextName=this.lbl5_next_sign_name,Next=this.lbl5_next_sign, NextYMD=this.lbl5_next_ymd} },
            {Planets.Jupiter, new DispItems{Star=this.lbl6_star, SignName=this.lbl6_sign_name, Sign=this.lbl6_sign, Reversed=this.lbl6_reverse, Sabian=this.lbl6_sabian, NextName=this.lbl6_next_sign_name, Next=this.lbl6_next_sign, NextYMD=this.lbl6_next_ymd} },
            {Planets.Saturn, new DispItems{Star=this.lbl7_star, SignName=this.lbl7_sign_name, Sign=this.lbl7_sign, Reversed=this.lbl7_reverse, Sabian=this.lbl7_sabian, NextName=this.lbl7_next_sign_name, Next=this.lbl7_next_sign, NextYMD=this.lbl7_next_ymd} },
            {Planets.Uranus, new DispItems{Star=this.lbl8_star, SignName=this.lbl8_sign_name, Sign=this.lbl8_sign, Reversed=this.lbl8_reverse, Sabian=this.lbl8_sabian, NextName=this.lbl8_next_sign_name, Next=this.lbl8_next_sign, NextYMD=this.lbl8_next_ymd} },
            {Planets.Neptune, new DispItems{Star=this.lbl9_star, SignName=this.lbl9_sign_name, Sign=this.lbl9_sign, Reversed=this.lbl9_reverse, Sabian=this.lbl9_sabian, NextName=this.lbl9_next_sign_name, Next=this.lbl9_next_sign, NextYMD=this.lbl9_next_ymd} },
            {Planets.Pluto, new DispItems{Star=this.lbl10_star, SignName=this.lbl10_sign_name, Sign=this.lbl10_sign, Reversed=this.lbl10_reverse, Sabian=this.lbl10_sabian, NextName=this.lbl10_next_sign_name, Next=this.lbl10_next_sign, NextYMD=this.lbl10_next_ymd} },
        };
    }
}


using System.Text.Json;
using System;
using StarViewerMaui.StarViewer;
using System.Linq;

namespace StarViewerMaui;

public partial class StarAspects2 : ContentPage
{
    /// <summary>
    ///     表示アイテム（関連天体）
    /// </summary>
    public class DispItemsStars
    {
        /// <summary>天体名</summary>
        public Label PlanetName { get; set; }
        /// <summary>天体</summary>
        public Label Planet { get; set; }
        /// <summary>星座名</summary>
        public Label SignName { get; set; }
        /// <summary>星座</summary>
        public Label Sign { get; set; }
        /// <summary>表示をクリアする。</summary>
        public void Clear()
        {
            this.PlanetName.Text = "";
            this.Planet.Text = "";
            this.SignName.Text = "";
            this.Sign.Text = "";
        }
    }

    /// <summary>
    ///     表示アイテム
    /// </summary>
    private class DispItems
    {
        /// <summary>開始日</summary>
        public Label StartYMD { get; set; }
        /// <summary>終了日</summary>
        public Label EndYMD { get; set; }
        /// <summary>アスペクト名</summary>
        public Label AspectName { get; set; }
        /// <summary>アスペクト</summary>
        public Label Aspect { get; set; }
        /// <summary>関連する星情報</summary>
        public List<DispItemsStars> Stars { get; set; } = new List<DispItemsStars>();
        /// <summary>表示するための枠組み</summary>
        public HorizontalStackLayout HStackLayout { get; set; }

        /// <summary>
        ///     表示をクリアする。
        /// </summary>
        public void Clear()
        {
            this.StartYMD.Text = "";
            this.EndYMD.Text = "";
            this.AspectName.Text = "";
            this.Aspect.Text = "";
            foreach (DispItemsStars clsStar in this.Stars)
            {
                clsStar.Clear();
            }
        }
    }

    public StarAspects2()
    {
        InitializeComponent();
    }

    private void Update()
    {
        DateTime dt = this.dtDate.Date.Value;
        Horoscope horo = new Horoscope();
        List<Horoscope.GroupAspectInfosMultiDate> lstGroupAspects = Horoscope.GroupAspectInfosMultiDate.GetGroupAspectInfos(dt.Date, 10, true);

        List<DispItems> lstDispItems = this.GetDispItems();
        for (int iLoop1 = 0; iLoop1 < 10; iLoop1++)
        {
            DispItems clsDisp = lstDispItems[iLoop1];
            clsDisp.Clear();
            if (iLoop1 < lstGroupAspects.Count)
            {
                Horoscope.GroupAspectInfosMultiDate clsGroupAspects = lstGroupAspects[iLoop1];
                clsDisp.StartYMD.Text = clsGroupAspects.DateTimeStart.ToString("yyyy/MM/dd");
                clsDisp.EndYMD.Text = clsGroupAspects.DateTimeEnd.ToString("yyyy/MM/dd");
                clsDisp.AspectName.Text = Horoscope.GroupAspectInfo.GetGroupAspectString(clsGroupAspects.GroupAspects);
                clsDisp.Aspect.Text = Horoscope.GroupAspectInfo.GetGroupAspectStringMark(clsGroupAspects.GroupAspects);
                clsDisp.HStackLayout.BackgroundColor = clsGroupAspects.GetGroupAspectColor();
                for (int iLoop2 = 0; iLoop2 < 4 && iLoop2 < clsGroupAspects.Planets.Count; iLoop2++)
                {
                    DispItemsStars clsDispStar = clsDisp.Stars[iLoop2];
                    Horoscope.HoroscopeInfo clsPlanet = clsGroupAspects.Planets[iLoop2];
                    clsDispStar.PlanetName.Text = clsPlanet.PlanetString;
                    clsDispStar.Planet.Text = Horoscope.HoroscopeInfo.GetPlanetsStringMark(clsPlanet.Planet);
                    clsDispStar.SignName.Text = Horoscope.HoroscopeInfo.GetSignString(clsPlanet.Sign);
                    clsDispStar.Sign.Text = Horoscope.HoroscopeInfo.GetSIgnEmoticon(clsPlanet.Sign);
                }
            }
        }
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        this.dtDate.Date = DateTime.Now;
        this.Update();
    }

    private void dtDate_Loaded(object sender, EventArgs e)
    {
        this.Update();
    }

    private void dtDate_DateSelected(object sender, DateChangedEventArgs e)
    {
        this.Update();
    }

    /// <summary>
    ///     表示アイテムを取得する。
    /// </summary>
    /// <returns></returns>
    private List<DispItems> GetDispItems()
    {
        return new List<DispItems>()
        {
            new DispItems() {
                StartYMD = this.lbl1_start_ymd,
                EndYMD = this.lbl1_end_ymd,
                AspectName = this.lbl1_aspect_name,
                Aspect = this.lbl1_aspect,
                HStackLayout = this.Stack1,
                Stars = new List<DispItemsStars>
                {
                    new DispItemsStars(){
                        PlanetName=this.lbl1_planet1_name,
                        Planet=this.lbl1_planet1,
                        SignName=this.lbl1_planet1_sign_name,
                        Sign=this.lbl1_planet1_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl1_planet2_name,
                        Planet=this.lbl1_planet2,
                        SignName=this.lbl1_planet2_sign_name,
                        Sign=this.lbl1_planet2_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl1_planet3_name,
                        Planet=this.lbl1_planet3,
                        SignName=this.lbl1_planet3_sign_name,
                        Sign=this.lbl1_planet3_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl1_planet4_name,
                        Planet=this.lbl1_planet4,
                        SignName=this.lbl1_planet4_sign_name,
                        Sign=this.lbl1_planet4_sign
                    }
                }
            },
            new DispItems()
            {
                StartYMD = this.lbl2_start_ymd,
                EndYMD = this.lbl2_end_ymd,
                AspectName = this.lbl2_aspect_name,
                Aspect = this.lbl2_aspect,
                HStackLayout = this.Stack2,
                Stars = new List<DispItemsStars>
                {
                    new DispItemsStars(){
                        PlanetName=this.lbl2_planet1_name,
                        Planet=this.lbl2_planet1,
                        SignName=this.lbl2_planet1_sign_name,
                        Sign=this.lbl2_planet1_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl2_planet2_name,
                        Planet=this.lbl2_planet2,
                        SignName=this.lbl2_planet2_sign_name,
                        Sign=this.lbl2_planet2_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl2_planet3_name,
                        Planet=this.lbl2_planet3,
                        SignName=this.lbl2_planet3_sign_name,
                        Sign=this.lbl2_planet3_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl2_planet4_name,
                        Planet=this.lbl2_planet4,
                        SignName=this.lbl2_planet4_sign_name,
                        Sign=this.lbl2_planet4_sign
                    }
                }
            },
            new DispItems()
            {
                StartYMD = this.lbl3_start_ymd,
                EndYMD = this.lbl3_end_ymd,
                AspectName = this.lbl3_aspect_name,
                Aspect = this.lbl3_aspect,
                HStackLayout = this.Stack3,
                Stars = new List<DispItemsStars>
                {
                    new DispItemsStars(){
                        PlanetName=this.lbl3_planet1_name,
                        Planet=this.lbl3_planet1,
                        SignName=this.lbl3_planet1_sign_name,
                        Sign=this.lbl3_planet1_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl3_planet2_name,
                        Planet=this.lbl3_planet2,
                        SignName=this.lbl3_planet2_sign_name,
                        Sign=this.lbl3_planet2_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl3_planet3_name,
                        Planet=this.lbl3_planet3,
                        SignName=this.lbl3_planet3_sign_name,
                        Sign=this.lbl3_planet3_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl3_planet4_name,
                        Planet=this.lbl3_planet4,
                        SignName=this.lbl3_planet4_sign_name,
                        Sign=this.lbl3_planet4_sign
                    }
                }
            },
            new DispItems()
            {
                StartYMD = this.lbl4_start_ymd,
                EndYMD = this.lbl4_end_ymd,
                AspectName = this.lbl4_aspect_name,
                Aspect = this.lbl4_aspect,
                HStackLayout = this.Stack4,
                Stars = new List<DispItemsStars>
                {
                    new DispItemsStars(){
                        PlanetName=this.lbl4_planet1_name,
                        Planet=this.lbl4_planet1,
                        SignName=this.lbl4_planet1_sign_name,
                        Sign=this.lbl4_planet1_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl4_planet2_name,
                        Planet=this.lbl4_planet2,
                        SignName=this.lbl4_planet2_sign_name,
                        Sign=this.lbl4_planet2_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl4_planet3_name,
                        Planet=this.lbl4_planet3,
                        SignName=this.lbl4_planet3_sign_name,
                        Sign=this.lbl4_planet3_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl4_planet4_name,
                        Planet=this.lbl4_planet4,
                        SignName=this.lbl4_planet4_sign_name,
                        Sign=this.lbl4_planet4_sign
                    }
                }
            },
            new DispItems()
            {
                StartYMD = this.lbl5_start_ymd,
                EndYMD = this.lbl5_end_ymd,
                AspectName = this.lbl5_aspect_name,
                Aspect = this.lbl5_aspect,
                HStackLayout = this.Stack5,
                Stars = new List<DispItemsStars>
                {
                    new DispItemsStars(){
                        PlanetName=this.lbl5_planet1_name,
                        Planet=this.lbl5_planet1,
                        SignName=this.lbl5_planet1_sign_name,
                        Sign=this.lbl5_planet1_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl5_planet2_name,
                        Planet=this.lbl5_planet2,
                        SignName=this.lbl5_planet2_sign_name,
                        Sign=this.lbl5_planet2_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl5_planet3_name,
                        Planet=this.lbl5_planet3,
                        SignName=this.lbl5_planet3_sign_name,
                        Sign=this.lbl5_planet3_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl5_planet4_name,
                        Planet=this.lbl5_planet4,
                        SignName=this.lbl5_planet4_sign_name,
                        Sign=this.lbl5_planet4_sign
                    }
                }
            },
            new DispItems()
            {
                StartYMD = this.lbl6_start_ymd,
                EndYMD = this.lbl6_end_ymd,
                AspectName = this.lbl6_aspect_name,
                Aspect = this.lbl6_aspect,
                HStackLayout = this.Stack6,
                Stars = new List<DispItemsStars>
                {
                    new DispItemsStars(){
                        PlanetName=this.lbl6_planet1_name,
                        Planet=this.lbl6_planet1,
                        SignName=this.lbl6_planet1_sign_name,
                        Sign=this.lbl6_planet1_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl6_planet2_name,
                        Planet=this.lbl6_planet2,
                        SignName=this.lbl6_planet2_sign_name,
                        Sign=this.lbl6_planet2_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl6_planet3_name,
                        Planet=this.lbl6_planet3,
                        SignName=this.lbl6_planet3_sign_name,
                        Sign=this.lbl6_planet3_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl6_planet4_name,
                        Planet=this.lbl6_planet4,
                        SignName=this.lbl6_planet4_sign_name,
                        Sign=this.lbl6_planet4_sign
                    }
                }
            },
            new DispItems()
            {
                StartYMD = this.lbl7_start_ymd,
                EndYMD = this.lbl7_end_ymd,
                AspectName = this.lbl7_aspect_name,
                Aspect = this.lbl7_aspect,
                HStackLayout = this.Stack7,
                Stars = new List<DispItemsStars>
                {
                    new DispItemsStars(){
                        PlanetName=this.lbl7_planet1_name,
                        Planet=this.lbl7_planet1,
                        SignName=this.lbl7_planet1_sign_name,
                        Sign=this.lbl7_planet1_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl7_planet2_name,
                        Planet=this.lbl7_planet2,
                        SignName=this.lbl7_planet2_sign_name,
                        Sign=this.lbl7_planet2_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl7_planet3_name,
                        Planet=this.lbl7_planet3,
                        SignName=this.lbl7_planet3_sign_name,
                        Sign=this.lbl7_planet3_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl7_planet4_name,
                        Planet=this.lbl7_planet4,
                        SignName=this.lbl7_planet4_sign_name,
                        Sign=this.lbl7_planet4_sign
                    }
                }
            },
            new DispItems()
            {
                StartYMD = this.lbl8_start_ymd,
                EndYMD = this.lbl8_end_ymd,
                AspectName = this.lbl8_aspect_name,
                Aspect = this.lbl8_aspect,
                HStackLayout = this.Stack8,
                Stars = new List<DispItemsStars>
                {
                    new DispItemsStars(){
                        PlanetName=this.lbl8_planet1_name,
                        Planet=this.lbl8_planet1,
                        SignName=this.lbl8_planet1_sign_name,
                        Sign=this.lbl8_planet1_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl8_planet2_name,
                        Planet=this.lbl8_planet2,
                        SignName=this.lbl8_planet2_sign_name,
                        Sign=this.lbl8_planet2_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl8_planet3_name,
                        Planet=this.lbl8_planet3,
                        SignName=this.lbl8_planet3_sign_name,
                        Sign=this.lbl8_planet3_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl8_planet4_name,
                        Planet=this.lbl8_planet4,
                        SignName=this.lbl8_planet4_sign_name,
                        Sign=this.lbl8_planet4_sign
                    }
                }
            },
            new DispItems()
            {
                StartYMD = this.lbl9_start_ymd,
                EndYMD = this.lbl9_end_ymd,
                AspectName = this.lbl9_aspect_name,
                Aspect = this.lbl9_aspect,
                HStackLayout = this.Stack9,
                Stars = new List<DispItemsStars>
                {
                    new DispItemsStars(){
                        PlanetName=this.lbl9_planet1_name,
                        Planet=this.lbl9_planet1,
                        SignName=this.lbl9_planet1_sign_name,
                        Sign=this.lbl9_planet1_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl9_planet2_name,
                        Planet=this.lbl9_planet2,
                        SignName=this.lbl9_planet2_sign_name,
                        Sign=this.lbl9_planet2_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl9_planet3_name,
                        Planet=this.lbl9_planet3,
                        SignName=this.lbl9_planet3_sign_name,
                        Sign=this.lbl9_planet3_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl9_planet4_name,
                        Planet=this.lbl9_planet4,
                        SignName=this.lbl9_planet4_sign_name,
                        Sign=this.lbl9_planet4_sign
                    }
                }
            },
            new DispItems()
            {
                StartYMD = this.lbl10_start_ymd,
                EndYMD = this.lbl10_end_ymd,
                AspectName = this.lbl10_aspect_name,
                Aspect = this.lbl10_aspect,
                HStackLayout = this.Stack10,
                Stars = new List<DispItemsStars>
                {
                    new DispItemsStars(){
                        PlanetName=this.lbl10_planet1_name,
                        Planet=this.lbl10_planet1,
                        SignName=this.lbl10_planet1_sign_name,
                        Sign=this.lbl10_planet1_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl10_planet2_name,
                        Planet=this.lbl10_planet2,
                        SignName=this.lbl10_planet2_sign_name,
                        Sign=this.lbl10_planet2_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl10_planet3_name,
                        Planet=this.lbl10_planet3,
                        SignName=this.lbl10_planet3_sign_name,
                        Sign=this.lbl10_planet3_sign
                    },
                    new DispItemsStars(){
                        PlanetName=this.lbl10_planet4_name,
                        Planet=this.lbl10_planet4,
                        SignName=this.lbl10_planet4_sign_name,
                        Sign=this.lbl10_planet4_sign
                    }
                }
            }
        };
    }
}
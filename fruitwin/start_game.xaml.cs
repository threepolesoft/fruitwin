using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace fruitwin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class start_game : ContentPage
    {

        public int task=0, m1, m2, point = 0;
        public float v1, v2, v3;
        public string opt="", fedback, ans;

        List<qs> qs_list = new List<qs>();

        public start_game()
        {
            InitializeComponent();

            btn_value_1.Clicked += Btn_value_1_Clicked;
            btn_value_2.Clicked += Btn_value_1_Clicked;

            btn_close.Clicked += Btn_close_Clicked;

            Title = "Fruit Win";

            string fn = "qs.json";

            var assembly = typeof(start_game).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{fn}");

            var reader = new StreamReader(stream);

            var js = reader.ReadToEnd();

            stream.Close();

            qs_list = JsonConvert.DeserializeObject<List<qs>>(js);

            in_que();
                       
        }

        private void Btn_close_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void Btn_value_1_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            check_ans(btn.Text);
        }

        public void in_que()
        {
                       
            img_wh_pic.Source = qs_list[task].q_ans.ToString() + ".jpg";

            btn_value_1.Text = qs_list[task].q_item[0].ToString();
            btn_value_2.Text = qs_list[task].q_item[1].ToString();

            ans = qs_list[task].q_ans.ToString();


            task++;
            lb_task.Text = "Task: " + task.ToString() + "/"+qs_list.Count;

        }

        public void check_ans(string vl)
        {
           
            if (vl.Equals(ans.ToString()))
            {
                point+=5;

                lb_answer.Text = "Right";
                lb_total_point.Text = "" + point.ToString();

                int p = Preferences.Get("score", 0);

                Preferences.Set("score", (p+point));
            }
            else
            {
                lb_answer.Text = "Wrong";
            }

            if (task < qs_list.Count)
            {
                in_que();
                
            }
            else
            {
                lb_question.Text = "Task is finish. Close and start again!";

                btn_value_1.IsVisible = false;
                btn_value_2.IsVisible = false;
            }
            
        }
    }

    public class DescendingInSorter: System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            return y.ToString().CompareTo(x.ToString());
        }
    }
}
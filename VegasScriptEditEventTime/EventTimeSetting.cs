using System.ComponentModel;
using System.Windows.Forms;

namespace VegasScriptEditEventTime
{
    public partial class EventTimeSetting : Form
    {
        public EventTimeSetting()
        {
            InitializeComponent();
        }

        public long StartTime
        {
            get { return GetStartTimeNanos(); }
            set { SetStartTimeNanos(value); }
        }

        public long TimeLength
        {
            get { return GetTimeLenthNanos(); }
            set { SetTimeLengthNanos(value); }
        }

        private long RoundNanos(long nanos)
        {
            return (nanos + 5000) / 10000;
        }

        private void SetStartTimeNanos(long nanos)
        {
            SetNanos(nanos, startTimeHour, startTimeMinute, startTimeSecond, startTimeMilliSecond);
        }

        private void SetTimeLengthNanos(long nanos)
        {
            SetNanos(nanos, timeLengthHour, timeLengthMinute, timeLengthSecond, timeLengthMilliSecond);
        }

        private void SetNanos(long nanos, TextBox hBox, TextBox mBox, TextBox sBox, TextBox msBox)
        {
            long roundNanos = RoundNanos(nanos);
            msBox.Text = (roundNanos % 1000).ToString();
            roundNanos = roundNanos / 1000;
            sBox.Text = (roundNanos % 60).ToString();
            roundNanos = roundNanos / 60;
            mBox.Text = (roundNanos % 60).ToString();
            roundNanos = roundNanos / 60;
            hBox.Text = roundNanos.ToString();
        }

        private long GetStartTimeNanos()
        {
            return GetNanos(startTimeHour, startTimeMinute, startTimeSecond, startTimeMilliSecond);
        }

        private long GetTimeLenthNanos()
        {
            return GetNanos(timeLengthHour, timeLengthMinute, timeLengthSecond, timeLengthMilliSecond);
        }

        private long GetNanos(TextBox hBox, TextBox mBox, TextBox sBox, TextBox msBox)
        {
            long nanos = long.Parse(hBox.Text) * 60;
            nanos = (nanos + long.Parse(mBox.Text)) * 60;
            nanos = (nanos + long.Parse(sBox.Text)) * 1000;
            return (nanos + long.Parse(msBox.Text)) * 10000;
        }


        private void ValidateNumberOnly(object sender, CancelEventArgs e)
        {
            errorProvider1.Clear();
            OKButton.Enabled = true;
            ValidateNumBox(startTimeHour, 24);
            ValidateNumBox(startTimeMinute, 60);
            ValidateNumBox(startTimeSecond, 60);
            ValidateNumBox(startTimeMilliSecond, 1000);
            ValidateNumBox(timeLengthHour, 24);
            ValidateNumBox(timeLengthMinute, 60);
            ValidateNumBox(timeLengthSecond, 60);
            ValidateNumBox(timeLengthMilliSecond, 1000);
        }
        
        private void ValidateNumBox(TextBox box, long max)
        {
            int tmp = 0;
            if (string.IsNullOrEmpty(box.Text))
            {
                box.Text = "0";
            }
            else if (!int.TryParse(box.Text, out tmp))
            {
                errorProvider1.SetError(box, "数値ではありません");
                OKButton.Enabled = false;
            }
            else if (tmp < 0 || tmp >= max)
            {
                errorProvider1.SetError(box, "範囲外の数値です");
                OKButton.Enabled = false;
            }

        }
    }
}

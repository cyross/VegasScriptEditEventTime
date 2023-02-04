using ScriptPortal.Vegas;
using System.Windows.Forms;
using VegasScriptHelper;

namespace VegasScriptEditEventTime
{
    public class EntryPoint: IEntryPoint
    {
        public void FromVegas(Vegas vegas)
        {
            VegasHelper helper = VegasHelper.Instance(vegas);

            try
            {
                TrackEvent trackEvent = helper.GetSelectedEvent();
                VegasDuration duration = helper.GetEventTime(trackEvent);

                EventTimeSetting dialog = new EventTimeSetting()
                {
                    StartTime = duration.StartTime.Nanos,
                    TimeLength = duration.Length.Nanos
                };
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    duration.StartTime.Nanos = dialog.StartTime;
                    duration.Length.Nanos = dialog.TimeLength;
                    helper.SetEventTime(trackEvent, duration);
                }
            }
            catch (VegasHelperTrackUnselectedException)
            {
                MessageBox.Show("トラックが選択されていません。");
            }
            catch (VegasHelperNoneEventsException)
            {
                MessageBox.Show("選択したトラック中にイベントが存在していません。");
            }
            catch(VegasHelperNoneSelectedEventException)
            {
                MessageBox.Show("イベントを選択していません。");
            }

        }
    }
}

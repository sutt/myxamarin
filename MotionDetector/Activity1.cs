using Android.App;
using Android.Hardware;
using Android.OS;
using Android.Widget;

namespace MotionDetector
{
    [Activity(Label = "MotionDetector", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity, ISensorEventListener
    {
        static readonly object _syncLock = new object();
        SensorManager _sensorManager;
        TextView _sensorTextView;
        TextView _sensorX;
        TextView _sensorY;
        TextView _sensorZ;
        //int _counter = 0;
        TextView _counter;
        public int counter = 0;

        public float[] xx = { };
        public float[] yy = { };
        public float[] zz = { };


        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
        {
            // We don't want to do anything here.
        }

        public void OnSensorChanged(SensorEvent e)
        {
            lock (_syncLock)
            {
                _sensorTextView.Text = string.Format("x={0:f}, y={1:f}, y={2:f}", e.Values[0], e.Values[1], e.Values[2]);

                

                int x = 0;
                float y = 0;
                y = e.Values[0];
                x = (int)y;

                string sx = "";
                for (int i = -20; i < 20; i++)
                {
                    if (e.Values[0] > i)
                    { sx += "|"; }
                    else
                    { sx += " "; }

                }
                _sensorX.Text = sx;

                sx = "";
                for (int i = -20; i < 20; i++)
                {
                    if (e.Values[1] > i)
                    { sx += "|"; }
                    else
                    { sx += " "; }

                }
                _sensorY.Text = sx;

                 sx = "";
                for (int i = -20; i < 20; i++)
                {
                    if (e.Values[2] > i)
                    { sx += "|"; }
                    else
                    { sx += " "; }

                }
                _sensorZ.Text = sx;

                counter += 1;
                _counter.Text = counter.ToString();

            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            _sensorManager = (SensorManager) GetSystemService(SensorService);
            _sensorTextView = FindViewById<TextView>(Resource.Id.accelerometer_text);
            _sensorX = FindViewById<TextView>(Resource.Id.accelerometer_textX);
            _sensorY = FindViewById<TextView>(Resource.Id.accelerometer_textY);
            _sensorZ = FindViewById<TextView>(Resource.Id.accelerometer_textZ);
            //_counter = 0;
            _counter = FindViewById<TextView>(Resource.Id.counter_text);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _sensorManager.RegisterListener(this,
                                            _sensorManager.GetDefaultSensor(SensorType.Accelerometer),
                                            SensorDelay.Ui);


        }

        protected override void OnPause()
        {
            base.OnPause();
            _sensorManager.UnregisterListener(this);
        }
    }
}
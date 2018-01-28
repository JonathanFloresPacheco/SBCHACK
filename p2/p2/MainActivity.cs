using Android.App;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using System.Net.Http.Headers;
using Android.Content;
using Android.Views.InputMethods;

namespace p2
{
    [Activity(Label = "Servicios de Cobro", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //SecondActivity mysecond = new SecondActivity(); 
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            TextView txtTitulo = FindViewById<TextView>(Resource.Id.txtTitle);
            //TextView txtUsuario = FindViewById<TextView>(Resource.Id.txtUsuario);
            TextView tvUser = FindViewById<TextView>(Resource.Id.txtVwUser);
            TextView txtContraseña = FindViewById<TextView>(Resource.Id.txtContraseña);
            TextView tvCont = FindViewById<TextView>(Resource.Id.txtVwPassword);
            Button btnIn = FindViewById<Button>(Resource.Id.btnIngresar);
            Button btnReg = FindViewById<Button>(Resource.Id.btnRegistrar);
            TextView tvPopUp = FindViewById<TextView>(Resource.Id.tvPopUp);
            //ImageView imageView = FindViewById<ImageView>(Resource.Id.);
            //btnScan = FindViewById<Button>(Resource.Id.btnScan);
            //txtResult = FindViewById<TextView>(Resource.Id.txtResult);

            //        editText.setOnFocusChangeListener(new OnFocusChangeListener() {
            //        @Override
            //        public void onFocusChange(View v, boolean hasFocus)
            //        {
            //            editText.post(new Runnable() {
            //                @Override
            //                public void run()
            //            {
            //                InputMethodManager imm = (InputMethodManager)getActivity().getSystemService(Context.INPUT_METHOD_SERVICE);
            //                imm.showSoftInput(editText, InputMethodManager.SHOW_IMPLICIT);
            //            }
            //        });
            //    }
            //});
            //    editText.requestFocus();

            btnIn.Click += (sender, e) =>
            {
                // Translate user’s alphanumeric phone number to numeric
                //string transUsuario= ServiciosPago.csUsers.ToNumber(tvUser.Text);
                //if (string.IsNullOrEmpty(transUsuario))
                //{
                //    //translatedPhoneWord.Text = string.Empty;
                //    tvPopUp.Text = "Debes ingresar un usuario";
                //    //myIntent.PutExtra("user", UserService.gettext().to);
                //}
                //else
                //{
                //    if (string.IsNullOrEmpty(transUsuario))
                //    {
                //        tvPopUp.Text = "Debes una contraseña";
                //    }
                //    else
                //    {

                var myIntent = new Intent(this, typeof(SecondActivity));
                StartActivity(myIntent);
                Finish();
                //}
                //translatedPhoneWord.Text = translatedNumber;
                //}
            };
            btnReg.Click += (sender, e) =>
            {
                var myIntent = new Intent(this, typeof(TreeActivity));
                StartActivity(myIntent);
                Finish();
            };
        }
    }
}


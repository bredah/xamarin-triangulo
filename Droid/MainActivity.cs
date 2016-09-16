using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using Android.Content;
using System.Text.RegularExpressions;

namespace triangulo.Droid
{
	[Activity (Label = "triangulo", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Recupera os elementos apresentados na tela
			EditText txtLadoA = FindViewById<EditText> (Resource.Id.valor_a);
			EditText txtLadoB = FindViewById<EditText> (Resource.Id.valor_b);
			EditText txtLadoC = FindViewById<EditText> (Resource.Id.valor_c);
			EditText txtResultado = FindViewById<EditText> (Resource.Id.valor_resultado);
			Button btnValidar = FindViewById<Button> (Resource.Id.buttonValidar);
			Button btnLimpar = FindViewById<Button> (Resource.Id.buttonLimpar);

			// Valida os valores informados
			btnValidar.Click += delegate {
				//this.dismissKeyboard();

				string strMensagemErro = "Somente valores numéricos, interios e positivios serão aceitos";
				string strMensagem = string.Empty;

				// Verifica se os campos foram preenchidos corretamente
				if (string.IsNullOrEmpty (txtLadoA.Text) || Regex.IsMatch (txtLadoA.Text, @"\D")) {
					strMensagem = strMensagemErro;
				}
				if (string.IsNullOrEmpty (txtLadoB.Text) || Regex.IsMatch (txtLadoB.Text, @"\D")) {
					strMensagem = strMensagemErro;
				}
				if (string.IsNullOrEmpty (txtLadoC.Text) || Regex.IsMatch (txtLadoC.Text, @"\D")) {
					strMensagem = strMensagemErro;
				}

				// Verifica qual tipo qual o tipo de triângulo
				int iLadoA;
				int.TryParse (txtLadoA.Text, out iLadoA);
				int iLadoB;
				int.TryParse (txtLadoB.Text, out iLadoB);
				int iLadoC;
				int.TryParse (txtLadoC.Text, out iLadoC);

				if (iLadoA == iLadoB && iLadoB == iLadoC) {
					strMensagem = "Triângulo Equilátero, 3 lados iguais";
				} else if (iLadoA == iLadoB || iLadoA == iLadoC || iLadoB == iLadoC) {
					strMensagem = "Triângulo Isósceles, 2 lados iguais";
				} else {
					strMensagem = "Triângulo Escaleno, nenhum lado igual";
				}

				txtResultado.Text = strMensagem;

			};
		

			// Efetua limpeza dos campos
			btnLimpar.Click += delegate {
				txtLadoA.Text = "";
				txtLadoA.RequestFocus ();
				txtLadoB.Text = "";
				txtLadoC.Text = "";
				txtResultado.Text = "";
			};
		}

		/// <summary>
		/// Fecha o teclado nativo
		/// </summary>
		public void dismissKeyboard ()
		{
			InputMethodManager imm = (InputMethodManager)this.GetSystemService (Context.InputMethodService);
			imm.HideSoftInputFromWindow (this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
		}
	}
}
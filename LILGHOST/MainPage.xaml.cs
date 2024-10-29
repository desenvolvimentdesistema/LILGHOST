namespace LILGHOST;

public partial class MainPage : ContentPage
{
    
	const int Gravidade = 5;
	const int AberturaMinima = 200;
	const int TempoEntreFrames=25;
	bool EstaMorto = false;
	double LarguraJanela=0;
	double AlturaJanela=0;
	int Velocidade= 10;
	const int ForcaPulo = 30;
	const int MaximoTempoPulando = 3; //frames
	bool EstaPulando = false;
	int TempoPulando = 0;
	int Score = 0;



	public MainPage()
	{
		InitializeComponent();
	}

	void AplicaGravidade()
	{
    	LilGhost.TranslationY += Gravidade;
	}
 	public async void Desenha()
 	{
		while (!EstaMorto)
		{
			if (EstaPulando)
				AplicaPulo();
			else			
			AplicaGravidade();
			GerenciaCanos ();
			if (VerificaColisao())
			{
				EstaMorto = true;
				SoundHelper.Play("deathsound.wav");
				FrameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(TempoEntreFrames);
		}
 	}

 	void Hello (object s, TappedEventArgs e)
 	{
		FrameGameOver.IsVisible = false;
		EstaMorto = false;
		Inicializar();
		Desenha();
		LabelCanos.Text = "Você superou " + Score.ToString("D3") + " obstáculos!!";
 	}

	void Inicializar()
	{
		LilGhost.TranslationY = 0;
		CanodeCima.TranslationX = -LarguraJanela;
		CanodeBaixo.TranslationX = -LarguraJanela;
		LilGhost.TranslationX = 0;
		LilGhost.TranslationY = 0;
		Score = 0;
		GerenciaCanos();
	}

	
		protected override void OnSizeAllocated ( double w, double h)
		{
			base.OnSizeAllocated ( w,h);
			LarguraJanela= w;
			AlturaJanela= h ;
		}

		void GerenciaCanos ()
		{
			CanodeCima.TranslationX -= Velocidade;

			CanodeBaixo.TranslationX -= Velocidade;

			if ( CanodeBaixo.TranslationX < -LarguraJanela)
			{
				CanodeBaixo.TranslationX = 0;

				CanodeCima.TranslationX =0;

				var alturaMax = -100;

				var alturaMin = -CanodeBaixo.HeightRequest;

				CanodeCima.TranslationY = Random.Shared.Next((int) alturaMin, (int) alturaMax);

				CanodeBaixo.TranslationY = CanodeCima.TranslationY + AberturaMinima + CanodeBaixo.HeightRequest;

				Score ++;

				LabelScore.Text = "Canos:" + Score.ToString("D3");


			}
		}

		bool VerificaColisaoTeto()
		{
			var minY = -AlturaJanela/2;
			if (LilGhost.TranslationY <= minY)
				return true;
			else
				return false;	
		}

		bool VerificaColisaoChao()
		{
			var maxY = AlturaJanela/2 - Floor.HeightRequest;
			if (LilGhost.TranslationY >= maxY)
				return true;
			else
				return false;	
		}

		bool VerificaColisao()
		{
			if(!EstaMorto)
			{
				if(VerificaColisaoTeto()  ||
					VerificaColisaoChao() ||
					VerificaColisaoCanoCima()||
					VerificaColisaoCanoBaixo())
					{
						return true;
					}
			}
			return false;
		}

		void AplicaPulo()
		{
			LilGhost. TranslationY -= ForcaPulo;
			TempoPulando++;
			if (TempoPulando >= MaximoTempoPulando)
			{
				EstaPulando = false;
				TempoPulando = 0;
			}
		}

		void OnGridClicked(object sender, TappedEventArgs a)
		{
			EstaPulando = true;
		}

		bool VerificaColisaoCanoCima()
		{
			var posHLilGhost = (LarguraJanela/2) - (LilGhost.WidthRequest/2);
			var posVLilGhost = (AlturaJanela/2) - (LilGhost.HeightRequest/2) + LilGhost.TranslationY;
			if (posHLilGhost >= Math.Abs(CanodeCima.TranslationX) - CanodeCima.WidthRequest &&
			posHLilGhost <= Math.Abs(CanodeCima.TranslationX) + CanodeCima.WidthRequest &&
			posVLilGhost <= CanodeCima.HeightRequest + CanodeCima.TranslationY)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		bool VerificaColisaoCanoBaixo()
		{
			var posHLilGhost = (LarguraJanela / 2) - (LilGhost.WidthRequest / 2);
			var posVLilGhost = (AlturaJanela / 2) - (LilGhost.HeightRequest / 2) + LilGhost.TranslationY;
			var yMaxCano = CanodeCima.HeightRequest + CanodeCima.TranslationY + AberturaMinima;
			if (posHLilGhost >= Math.Abs(CanodeBaixo.TranslationX) - CanodeBaixo.WidthRequest &&
		 	posHLilGhost <= Math.Abs(CanodeBaixo.TranslationX) + CanodeBaixo.WidthRequest &&
		 	posVLilGhost >= yMaxCano)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	
}


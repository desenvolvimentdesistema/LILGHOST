namespace LILGHOST;

public partial class MainPage : ContentPage
{
    
	const int Gravidade =1 ;
	const int TempoEntreFrames=25;
	bool EstaMorto = false;
	double LarguraJanela=0;
	double AlturaJanela=0;
	int Velocidade= 20;



	public MainPage()
	{
		InitializeComponent();
	}

	void AplicaGravidade()
	{
    	lilghost.TranslationY += Gravidade;
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
				FrameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(TempoEntreFrames);
		}
 	}

 	void Oi (object s, TappedEventArgs e)
 	{
		FrameGameOver.IsVisible = false;
		EstaMorto = false;
		Inicializar();
		Desenha();
 	}

	void Inicializar()
	{
		lilghost.TranslationY = 0;
	}

	
		protected override void OnSizeAllocated ( double w, double h)
		{
			base.OnSizeAllocated ( w,h);
			LarguraJanela= w;
			AlturaJanela= h ;
		}

		void GerenciaCanos ()
		{
			canocima.TranslationX -= Velocidade;
			canobaixo.TranslationX -= Velocidade;
			if ( canobaixo.TranslationX < -LarguraJanela)
			{
				canobaixo.TranslationX = 0;
				canocima.TranslationX =0;
			}
		}

		bool VerificaColisaoTeto()
		{
			var maxY = -AlturaJanela/2;
			if (lilghost.TranslationY <=minY)
				return true;
			else
				return false;	
		}

		bool VerificaColisaoChao()
		{
			var maxY = AlturaJanela/2 - floor.HeightRequest;
			if (lilghost.TranslationY >=maxY)
				return true;
			else
				return false;	
		}

		bool VerificaColisao()
		{
			if(!EstaMorto)
			{
				if(VerificaColisaoTeto()  ||
					VerificaColisaoChao())
					{
						return true;
					}
					{
						return false;
					}
			}
		}

		void AplicaPulo()
		{
			imagelilghost. TranslationY -= ForcaPulo;
			TempoPulando++;
			if (tempoPulando >= maxTempoPulando)
			{
				EstaPulando = false;
				TempoPulando = 0;
			}
		}

		void OnGridClicked(object sender, TappedEventArgs a)
		{
			EstaPulando = true;
		}
	
}


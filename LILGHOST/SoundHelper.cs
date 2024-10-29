using System.Linq.Expressions;
using Plugin.Maui.Audio;

public class SoundHelper
{

    public static void Play(sring nomeArquivoWav)
    {
        Task.Run(async() =>
        {
            var AudioFX = await FileSystem.OpenAppPackegFileAsync(nomeArquivoWav);
            var AudioPlayer = AudioManager.Current.CreatePlayer(AudioFX);
            AudioPlayer.Play();
        });
    }

}
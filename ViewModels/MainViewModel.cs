using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using PropertyChanged;

namespace BmpToJpg.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        #region Field
        private FolderBrowserDialog FBD;
        #endregion

        #region Constructor
        public MainViewModel()
        {

        }
        #endregion

        #region Property
        /// <summary>
        /// 변환할 이미지가 있는 폴더 경로
        /// </summary>
        public string SelectedPath { get; set; } = null;
        /// <summary>
        /// 변환된 이미지가 저장될 폴더 경로
        /// </summary>
        public string SavePath { get; set; } = null;
        /// <summary>
        /// 압축률 
        /// </summary>
        public long Quality { get; set; } = 0;
        #endregion

        #region Button_Method
        public void Btn_SelectFolder()
        {
            FBD = new FolderBrowserDialog();
            FBD.ShowDialog();

            SelectedPath = FBD.SelectedPath;

            // 초기경로 DeskTop
            /*
            if (SelectedPath == "")
            {
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }

            // 기본 경로를 txt로 저장
            
            using (StreamWriter sw = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + @"/DefaultPath.txt"))
            {
                sw.WriteLine(selectedPath);
            }
            */
        }

        public void Btn_SaveFolder()
        {
            FBD = new FolderBrowserDialog();
            FBD.ShowDialog();

            SavePath = FBD.SelectedPath;
        }
        #endregion

        #region Method
        public void Bmp2Jpg()
        {
            DirectoryInfo di = new DirectoryInfo(SelectedPath);
            // TODO : 2. 이미지 경로에 있는 이미지 파일을 가져오기
            try
            {
                foreach (var item in di.GetFiles())
                {
                    // 파일이 없을 때
                    if (item == null)
                    {
                        // 파일이 없습니다.
                        MessageBox.Show("변환할 파일이 없습니다.");
                    }

                    // 파일이 있을 때
                    using (Bitmap inputImage = new Bitmap($@"{SelectedPath}\{item}"))
                    {
                        ImageCodecInfo info = ImageCodecInfo.GetImageEncoders().Where(a => a.MimeType.Contains("jpeg")).First();
                        EncoderParameters eParams = new EncoderParameters(1);
                        eParams.Param[0] = new EncoderParameter(Encoder.Quality, 100-Quality);
                        inputImage.Save($@"{SavePath}\{item}", info, eParams);
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion
    }
}

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        }

        /// <summary>
        /// 텍스트 박스에 0~9까지만 입력되게함
        /// </summary>
        /// <param name="e"></param>
        internal void TextBoxIntCheck(System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        /// <summary>
        /// 저장할 이미지 경로를 받아오는 메소드
        /// </summary>
        public void Btn_SaveFolder()
        {
            FBD = new FolderBrowserDialog();
            FBD.ShowDialog();

            SavePath = FBD.SelectedPath;
        }
        #endregion

        #region Method
        /// <summary>
        /// 이미지를 JPG로 변환하는 프로그램
        /// </summary>
        public void Bmp2Jpg()
        {
            // TODO : 2. 이미지 경로에 있는 이미지 파일을 가져오기
            try
            {
                if (SelectedPath == null)
                {
                    throw new Exception("압축할 이미지 경로를 설정해주세요.");
                }

                DirectoryInfo di = new DirectoryInfo(SelectedPath);
                

                if(SavePath == null)
                {
                    throw new Exception("압축한 이미지 경로를 설정해주세요.");
                }

                foreach (var item in di.GetFiles())
                {
                    // 파일이 없을 때
                    if (item == null)
                    {
                        throw new Exception("압축할 이미지가 없습니다.");
                    }
                    // 파일이 있을 때
                    else
                    {
                        // 파일명에서 확장자를 제거하고 가져옴
                        string itemName = item.Name.Substring(0, item.Name.LastIndexOf("."));

                        using (Bitmap inputImage = new Bitmap($@"{SelectedPath}\{item}"))
                        {
                            ImageCodecInfo info = ImageCodecInfo.GetImageEncoders().Where(a => a.MimeType.Contains("jpeg")).First();
                            EncoderParameters eParams = new EncoderParameters(1);
                            eParams.Param[0] = new EncoderParameter(Encoder.Quality, 100 - Quality);
                            inputImage.Save($@"{SavePath}\{itemName}.jpg", info, eParams);
                        }

                        MessageBox.Show("압축 완료되었습니다.");
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }
        #endregion
    }
}

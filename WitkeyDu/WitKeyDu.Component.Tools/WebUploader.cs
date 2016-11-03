using System;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace WitKeyDu.Component.Tools
{
    #region 辅助类
    /// <summary>
    /// 上传消息类
    /// </summary>
    public class UploaderResult
    {
        public UploaderResult()
        {
            this.Filish = false;
            this.Chunk = 0;
            this.Chunks = 0;
            this.FileExtist = false;
            this.ChunkNum = new List<int>();
        }
        /// <summary>
        /// 文件名称、当上传完成时将返回最终的文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件是否全部上传完成
        /// </summary>
        public bool Filish { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 如果为分块上传、返回当前的分块索引
        /// </summary>
        public int Chunk { get; set; }

        /// <summary>
        /// 总的分块大小
        /// </summary>
        public int Chunks { get; set; }

        /// <summary>
        /// 上传的文件是否已经存在于服务器
        /// </summary>
        public bool FileExtist { get; set; }

        /// <summary>
        /// 服务器已经存在的区块序号
        /// </summary>
        public List<int> ChunkNum { get; set; }
    }
    /// <summary>
    /// 上传字典
    /// </summary>
    public class WebUploaderField
    {
        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 文件保存名称
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// 文件Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// 文件分块大小
        /// </summary>
        public long Chunksize { get; set; }
        /// <summary>
        /// 文件Md5
        /// </summary>
        public string Md5 { get; set; }
        /// <summary>
        /// 分块总大小
        /// </summary>
        public int Chunks { get; set; }
        /// <summary>
        /// 当前分块
        /// </summary>
        public int Chunk { get; set; }
        /// <summary>
        /// 文件最终保存名称
        /// </summary>
        public string Filesavename { get; set; }
        /// <summary>
        /// 文件最终路径+文件名
        /// </summary>
        public string FileSavePath { get { return Path + Filename; } }
        /// <summary>
        /// 分片保存的名称
        /// </summary>
        public string FileSaveChunkPath
        {
            get
            {
                string strpath = "";
                if (!string.IsNullOrWhiteSpace(Md5))
                {
                    strpath = FileSavePath + "_" + Md5 + "_";
                }
                else
                {
                    strpath = FileSavePath + "_";
                }
                return strpath;
            }
        }
        /// <summary>
        /// 文件
        /// </summary>
        public HttpPostedFileBase File { get; set; }
    }
    #endregion

    /// <summary>
    /// webuploader后台操作类
    /// </summary>
    public class WebUploader
    {
        private const string chunkFlag = "startcheckchunk";//分块检测标志
        private static ConcurrentDictionary<string, List<int>> allChunks =
            new ConcurrentDictionary<string, List<int>>();//指示文件是否上传完成
        private WebUploaderField _webUploaderField = null;
        public WebUploader()
        {

        }
        public WebUploader(HttpRequestBase request, string savePath, string saveFileName)
        {
            InitWebUploaderField(request, savePath, saveFileName);
        }

        /// <summary>
        /// 初始化文件信息集合
        /// </summary>
        /// <param name="request"></param>
        private void InitWebUploaderField(HttpRequestBase request, string savePath, string saveFileName)
        {
            _webUploaderField = new WebUploaderField()
            {
                Path = AppDomain.CurrentDomain.BaseDirectory + savePath + "\\",
                Filename = request.Form["filename"] == null ? null : request.Form["filename"].ToString(),
                Id = request.Form["id"] == null ? null : request.Form["id"].ToString(),
                Size = request.Form["size"] == null ? 0 : Convert.ToInt64(request.Form["size"]),
                Chunksize = request.Form["chunksize"] == null ? 0 : Convert.ToInt64(request.Form["chunksize"]),
                Md5 = request.Form["md5"] == null ? null : request.Form["md5"].ToString(),
                Chunks = request.Form["chunks"] == null ? 0 : Convert.ToInt32(request.Form["chunks"]),
                Chunk = request.Form["chunk"] == null ? 0 : Convert.ToInt32(request.Form["chunk"])
                //,FileSavePath = AppDomain.CurrentDomain.BaseDirectory + savePath + "\\" + saveFileName
            };


        }

        /// <summary>
        /// 断点续传检测、MD5检测
        /// </summary>
        /// <returns></returns>
        public UploaderResult ProcessCheck(HttpRequestBase request, string saveFileName, string savepath = null
            , Func<HttpPostedFileBase, string> setfilename = null, Func<string, bool> md5check = null)
        {
            long o = _webUploaderField.Size % _webUploaderField.Chunksize;



            int chunks = _webUploaderField.Chunksize != 0 ?
                Convert.ToInt32(_webUploaderField.Size / _webUploaderField.Chunksize) : 1;
            if (o != 0)
            {
                chunks = chunks + 1;
            }
            UploaderResult obj = new UploaderResult();
            if (md5check(_webUploaderField.Md5))
            {
                obj.Filish = true;
                obj.FileExtist = true;//表明文件已存在于服务器
                obj.Message = "文件已存在于服务器、跳过上传";
                return obj;
            }
            WebUploader.allChunks[_webUploaderField.FileSavePath] = new List<int>();
            int j = 0;
            for (int i = 0; i < chunks; i++)
            {
                if (File.Exists(_webUploaderField.FileSaveChunkPath + i.ToString()))
                {
                    obj.ChunkNum.Add(i);//服务器已经存在的区块编号
                    WebUploader.allChunks[_webUploaderField.FileSavePath].Add(i);
                    j++;
                }
            }
            obj.Filish = false;
            obj.Message = string.Format("服务器已经存在区块数量{0},总区块数量{1},占比{2}%", j
                , _webUploaderField.Chunks, _webUploaderField.Chunks != 0 && j != 0
                ? Convert.ToDouble(Convert.ToDouble(j) / Convert.ToDouble(_webUploaderField.Chunks)) * 100 : 0);
            return obj;
        }

        public UploaderResult Process(HttpRequestBase request, string saveFileName, string savepath = null
            , Func<HttpPostedFileBase, string> setfilename = null, Func<string, bool> md5check = null)
        {
            InitWebUploaderField(request, savepath, saveFileName);

            #region 跳转到分片检测
            if (request[chunkFlag] != null && Convert.ToBoolean(request[chunkFlag]))
            {
                return ProcessCheck(request, saveFileName, savepath, setfilename, md5check);
            }
            #endregion

            UploaderResult obj = new UploaderResult();

            _webUploaderField.File = request.Files.Count == 0 ? null : request.Files[0];

            _webUploaderField.Filename = _webUploaderField.File == null ? "" : _webUploaderField.File.FileName;

            if (_webUploaderField.File == null)
            {
                obj.Message = "上传文件为空";
                return obj;
            }

            if (!Directory.Exists(_webUploaderField.Path))
            {
                Directory.CreateDirectory(_webUploaderField.Path);
            }

            if (_webUploaderField.Chunks != 0)
            {
                WebUploader.allChunks[_webUploaderField.FileSavePath].Add(_webUploaderField.Chunk);
                //_webUploaderField.File.SaveAs(_webUploaderField.FileSaveChunkPath + _webUploaderField.Chunk);

                try
                {
                    Stream stream = _webUploaderField.File.InputStream;
                    byte[] b = new byte[stream.Length];//新建一个字节数组
                    stream.Read(b, 0, b.Length);
                    // 设置当前流的位置为流的开始
                    stream.Seek(0, SeekOrigin.Begin);

                    using (FileStream fstream = new FileStream(_webUploaderField.FileSaveChunkPath + _webUploaderField.Chunk, FileMode.Create, FileAccess.Write))
                    {
                        fstream.Write(b, 0, b.Length);
                    }
                }
                catch { }

                obj.Filish = false;
                obj.Message = string.Format("当前上传区块{0},总区块：{1},模式：分片上传", _webUploaderField.Chunk, _webUploaderField.Chunks);


                if (WebUploader.allChunks[_webUploaderField.FileSavePath].Count == _webUploaderField.Chunks)
                {
                    var file = WebUploader.allChunks.FirstOrDefault().Key;//F:\Simple project\文件上传\完整版断点续传、秒传，支持超大大大文件\UploaderTest\VideoPath\20151230\CrossGene-和我玩吧-[68mtv.com].mp4
                    var currentSavePath = file.Substring(0,file.LastIndexOf('\\'));
                    var fileSuffix = file.Substring(file.LastIndexOf("."), file.Length - file.LastIndexOf("."));

                    //saveFileName = saveFileName + fileSuffix;
                    saveFileName = (currentSavePath +"\\"+ saveFileName + fileSuffix);

                    using (FileStream fsw = new FileStream(saveFileName, FileMode.Create, FileAccess.Write, FileShare.Write))
                    {
                        BinaryWriter bw = new BinaryWriter(fsw);
                        for (int j = 0; j < _webUploaderField.Chunks; j++)
                        {
                            if (File.Exists(_webUploaderField.FileSaveChunkPath + j))
                            {
                                bw.Write(File.ReadAllBytes(_webUploaderField.FileSaveChunkPath + j));
                                bw.Flush();
                                File.Delete(_webUploaderField.FileSaveChunkPath + j);
                            }
                            else
                            {
                            }
                        }
                        //bw.Flush();
                    }
                    List<int> filekeyvalue = new List<int>();
                    WebUploader.allChunks.TryRemove(_webUploaderField.FileSavePath, out filekeyvalue);
                    obj.Filish = true;
                    obj.Message = string.Format("上传已全部完成");
                }
            }
            else
            {
                _webUploaderField.File.SaveAs(_webUploaderField.FileSavePath);
                obj.Filish = true;
                obj.Message = "上传已完成、模式 no chunk";
            }
            return obj;
        }
    }
}

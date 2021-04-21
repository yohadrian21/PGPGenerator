using PgpCore;
using PGPGenerator.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFExtrasPlus.Soap;

namespace PGPGenerator
{
    class GlobalVariable
    {
        public static string userName { get; set; }
        public static string passWord { get; set; }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("==========================PGP KEY GENERATOR==========================");
                //CreateDir(@"D:\PGPKey");
                //CreateFile(@"D:\PGPKey", "tes");
                var stopWatch = Stopwatch.StartNew();
                string userName = "";
                string passWord = "";
                string TargetUserPassDir = M10Framework.getTargetUserPassDir() + "";
                string TargetPublicDir = M10Framework.getTargetPublicDir() + "";
                string TargetPrivateDir = M10Framework.getTargetPrivateDir() + "";
                //var Proses = new Process();
                //Proses.Processing();
                var help = (from p in args
                            where p.Contains("HELP") || p.Contains("help") || p.Contains("H") || p.Contains("h")
                            select p).FirstOrDefault();
                foreach (var a in args)
                {
                    Console.WriteLine(a + " ");
                }
                if (help != null)
                {
                    Help();
                }

                if (args.Length == 0)
                {
                    //GlobalVariable = "y";
                    //GlobalVariable.price = "y";
                    //GlobalVariable.promo = "y";
                }
                else
                {
                    Update(args);
                }

                //Proses.Processing();
                if (GlobalVariable.userName != "")
                {
                    Console.WriteLine("Please Input Username");
                    GlobalVariable.userName = Console.ReadLine();

                }
                else
                {

                }
                if (GlobalVariable.passWord != "")
                {
                    Console.WriteLine("Please Input Password");
                    //GlobalVariable.passWord = Console.ReadLine();
                    Console.CursorVisible = true;
                    StringBuilder passwordBuilder = new StringBuilder();
                    bool continueReading = true;
                    char newLineChar = '\r';
                    while (continueReading)
                    {
                        ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                        char passwordChar = consoleKeyInfo.KeyChar;

                        if (passwordChar == newLineChar)
                        {
                            continueReading = false;
                        }
                        else
                        {
                            passwordBuilder.Append(passwordChar.ToString());
                        }
                    }
                    Console.WriteLine();
                    //Console.Write("Your password in plain text is {0}", passwordBuilder.ToString());
                    GlobalVariable.passWord = passwordBuilder.ToString();
                    Console.CursorVisible = true;
                }
                else
                {

                }

                //GetUserPass();
                Console.WriteLine("==========================START CREATING DIRECTORY==========================");
                string DirPublic = CreateDir(M10Framework.getTargetPublicDir());
                string DirPrivate = CreateDir(M10Framework.getTargetPrivateDir());
                string DirUserPass = CreateDir(M10Framework.getTargetUserPassDir());
                string FileNameUP = "UserPass";
                string FileUserPass = CreateFile(DirUserPass, FileNameUP);
                Console.WriteLine("==========================START CREATING DIRECTORY==========================");

                Console.WriteLine("==========================START ENCRYPTING PASSWORD==========================");
                string encodedpassWord = Common.EncodeTo64(GlobalVariable.passWord);
                Console.WriteLine("==========================FINISH ENCRYPTING PASSWORD==========================");
                Console.WriteLine("==========================WRITE USERNAME AND PASSWORD==========================");
                string Content = "Username : " + GlobalVariable.userName + "\n" + "Password : " + encodedpassWord;
                WriteTexttoFile(FileUserPass, Content);


                Console.WriteLine("==========================FINISH WRITE USERNAME AND PASSWORD==========================");

                Console.WriteLine("==========================START GENERATING PUBLIC KEY AND PRIVATE KEY==========================");
                using (PGP pgp = new PGP())
                {
                    pgp.GenerateKey(@""+DirPublic+"\\public.asc", @"" + DirPrivate + "\\private.asc", GlobalVariable.userName, GlobalVariable.passWord);
                 }
                Console.WriteLine("==========================FINISH GENERATING PUBLIC KEY AND PRIVATE KEY==========================");

                Console.WriteLine("Finish in {0}", stopWatch.Elapsed.TotalSeconds);

            }
            catch
            (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        static void Help()
        {
            try
            {
                FileInfo file = new FileInfo("HELP.txt");
                StreamReader streamReader = new StreamReader(file.Directory + "\\Help\\HELP.txt");
                string result = streamReader.ReadToEnd();

                Console.WriteLine(result);
                Console.ReadLine();
                Environment.Exit(0);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        static void Update(string[] args)
        {
            try
            {
                var userName = (from p in args
                                where p.Contains("u") || p.Contains("U")
                                select p).FirstOrDefault();
                if (userName != null)
                {
                    var uN = userName.Split('=')[1];

                    GlobalVariable.userName = uN;

                }
                else
                {
                    GlobalVariable.userName = "";

                }
                var passWord = (from p in args
                                where p.Contains("p") || p.Contains("p")
                                select p).FirstOrDefault();
                if (passWord != null)
                {
                    var pw = passWord.Split('=')[1];
                    GlobalVariable.passWord = pw;

                }
                else

                {
                    GlobalVariable.passWord = "";

                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);

            }
         
        }

        static void GetUserPass()
        {

            try
            {
                String userdomain = null;
                String userrun = null;
                Console.WriteLine("Started get user pass");
                //WriteTexttoFile(GlobalVariable.FileNamePath, "Started get user pass");

                CSoapHeader.Soap.IPClient = Common.InfoLAN()[Common.INFOHARDWARE.IPCLIENT];
                CSoapHeader.Soap.MACAddress = Common.InfoLAN()[Common.INFOHARDWARE.MACADDRESS];
                CSoapHeader.Soap.ApplicationID = M10Framework.getApplicationID();
                CSoapHeader.Soap.CompanyId = M10Framework.getCompanyID();
                M10BusinessProcess.Privilege[] pr = null;
                //untuk mendapatkan account windows

                userdomain = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                userrun = userdomain.Split('\\')[1];
                //userName = "TESTING";
                //cid = 1;

                CSoapHeader.ClientWs.InnerChannel.SetHeader("SOAPDataContract", CSoapHeader.Soap);
                //testing
                //CSoapHeader.Soap.SessionId = CSoapHeader.ClientWs.AuthenticateByUserName("applicationuser@mitra10.com", null, null);
                //live
                CSoapHeader.Soap.SessionId = CSoapHeader.ClientWs.AuthenticateByUserName(userrun + "@mitra10.com", null, null);


                //Authorize access
                Console.WriteLine("Started authorize user pass");
                //WriteTexttoFile(GlobalVariable.FileNamePath, "Started authorize user pass");

                bool bs = CSoapHeader.ClientWs.Authorize(Convert.ToInt32(M10Framework.getModuleID()), ref pr);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            }
        public static string CreateDir(string targetDir)
        {
            //string fileNameRet;
            try
            {
                //string fileNamedir = Directory.GetCurrentDirectory() + @"\ProcesLog\";
                //string fileNamedir = AppDomain.CurrentDomain.BaseDirectory + @"TextBeforeEncrypt\";
                
                string Date = DateTime.Now.ToString("ddMMyyyyHHmmss");
                string fileNamedir = @""+targetDir;
                string fileName = "";

                if (!Directory.Exists(fileNamedir))
                {
                    Directory.CreateDirectory(fileNamedir);
                }
                return fileName=fileNamedir;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed " + ex.Message);


                //SendEmail("Failed in Console Jubelio : " + ex.Message + " on " + DateTime.Now + "\n");
                throw new Exception(ex.Message);
            }
        }
        public static string CreateFile(string targetDir, string targetFileName)
        {
            //string fileNameRet;
            try
            {
                
                string Date = DateTime.Now.ToString("ddMMyyyyHHmmss");
                string Sequence = "1";
                string fileName = "";
                if (targetFileName != "")
                {

                    string Extension = ".txt";

                    string localfileexist = targetFileName + "_" + DateTime.Now.ToString("ddMMyyyy");
                    //fileName = fileNamedir;
                    var directory = new DirectoryInfo(targetDir);
                    var myFile = (from f in directory.GetFiles()
                                  where f.Name.Contains(DateTime.Now.ToString("ddMMyyyy"))
                                  && f.Name.Contains(Extension)
                                  orderby f.LastWriteTime descending
                                  select f).FirstOrDefault();
                    if (myFile != null)
                    {
                        Sequence = myFile.Name.Replace(localfileexist, "").Replace(Extension, "").Replace("_","");
                        int Seq = Convert.ToInt32(Sequence);
                        Sequence = (Seq + 1).ToString();
                        fileName = targetDir + localfileexist + "_" + Sequence + Extension;

                    }
                    else
                    {
                        fileName = targetDir + localfileexist + "_1" + Extension;

                    }
                    int fileCnt = 0;

                    fileName
                        = @""+targetDir+"\\"+fileName.Replace(targetDir, "");
                    //var pathfileName = fileNamedir + "NAMA NANTI DISINI_" + ProductCategory + "_" + FormatType + "_" + GCIFID + "_" + Date + "_" + Sequence + "_";
                    //fileName = pathfileName + Extension;
                    FileStream fs = File.Create(fileName);
                    fs.Close();
                }
                //fileNameRet = fileName;
                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed " + ex.Message);


                //SendEmail("Failed in Console Jubelio : " + ex.Message + " on " + DateTime.Now + "\n");
                throw new Exception(ex.Message);
            }
        }

        static void WriteTexttoFile(string fileName, string content)
        {
            try
            {

                StreamWriter sw = File.AppendText(fileName);
                sw.WriteLine(content);
                //sw.WriteLine(" tes2");
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed " + ex.Message);


                //SendEmail("Failed in Console Jubelio : " + ex.Message + " on " + DateTime.Now + "\n");
                throw new Exception(ex.Message);
            }
        }

    }
}

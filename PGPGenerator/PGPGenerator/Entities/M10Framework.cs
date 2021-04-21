using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Configuration;
using System.Data;
using System.Configuration;
using WCFExtrasPlus;

namespace PGPGenerator.Entities
{
    public static class M10Framework
    {
        #region url-api
        public static string getItemAPI()
        {
            return ConfigurationManager.AppSettings["getItemAPI"];
        }
        public static string getItemJub()
        {
            return ConfigurationManager.AppSettings["getItemJub"];
        }
        public static string getItemNAVAPIJub()
        {
            return ConfigurationManager.AppSettings["getItemNAVAPIJub"];
        }
        public static string getItemNAVAPICap()
        {
            return ConfigurationManager.AppSettings["getItemNAVAPICap"];
        }
        public static string getChannelAPI()
        {
            return ConfigurationManager.AppSettings["getChannelAPI"];
        }

        public static string getChannelJub()
        {
            return ConfigurationManager.AppSettings["getChannelJub"];
        }
        public static string getPromoAPI()
        {
            return ConfigurationManager.AppSettings["getPromoAPI"];
        }

        public static string sendItemStockAPI()
        {
            return ConfigurationManager.AppSettings["sendItemStockAPI"];
        }

        public static string sendItemStockJub()
        {
            return ConfigurationManager.AppSettings["sendItemStockJub"];
        }

        public static string sendItemPriceAPI()
        {
            return ConfigurationManager.AppSettings["sendItemPriceAPI"];
        }

        public static string sendItemPriceJub()
        {
            return ConfigurationManager.AppSettings["sendItemPriceJub"];
        }

        public static string sendPromoAPI()
        {
            return ConfigurationManager.AppSettings["sendPromoAPI"];
        }

        public static string sendPromoJub()
        {
            return ConfigurationManager.AppSettings["sendPromoJub"];
        }
        #endregion
        
        public static int getApplicationID()
        {
            return Convert.ToInt16(ConfigurationManager.AppSettings["ApplicationID"]);
        }

        public static int getCompanyID()
        {
            return Convert.ToInt16(ConfigurationManager.AppSettings["CompanyID"]);
        }

        public static Int16 getModuleID()
        {
            return Convert.ToInt16(ConfigurationManager.AppSettings["ModuleID"]);
        }

        public static string getSiteID()
        {
            return ConfigurationManager.AppSettings["SiteId"];
        }

        public static int getPageSize()
        {
            return Convert.ToInt16(ConfigurationManager.AppSettings["PageSize"]);
        }

        public static int getRoundPrice()
        {
            return Convert.ToInt16(ConfigurationManager.AppSettings["RoundPrice"]);
        }

        public static int getDelayRetry()
        {
            return Convert.ToInt16(ConfigurationManager.AppSettings["DelayRetry"]);
        }
        public static int getDelayPromoTimeRetry()
        {
            return Convert.ToInt16(ConfigurationManager.AppSettings["DelayPromoTime"]);
        }
        public static string getEventLog()
        {
            return ConfigurationManager.AppSettings["EventLog"];
        }

        public static string getParamKeyCondition()
        {
            return ConfigurationManager.AppSettings["ConditionID"];
        }

        public static string getParamKeyQuery()
        {
            return ConfigurationManager.AppSettings["QueryID"];
        }

        public static string EmailAddress()
        {
            return ConfigurationManager.AppSettings["EmailAddress"];
        }

        public static string EmailBody()
        {
            return ConfigurationManager.AppSettings["EmailBody"];
        }

        public static string EmailFooter()
        {
            return ConfigurationManager.AppSettings["EmailFooter"];
        }
        public static string getTargetPublicDir()
        {
            return ConfigurationManager.AppSettings["TargetPublicDir"];
        }
        public static string getTargetPrivateDir()
        {
            return ConfigurationManager.AppSettings["TargetPrivateDir"];
        }
        public static string getTargetUserPassDir()
        {
            return ConfigurationManager.AppSettings["TargetUserPassDir"];
        }
        public static string GetQuery(int ApplicationID, int CompanyID, string ParamKey, string ApplicationConfigKey)
        {
            string Query = string.Empty;
            try
            {
                string ErrorMessage = string.Empty;
                DataTable dt = new DataTable("dtQuery");

                dt = CSoapHeader.ClientWs.GetApplicationConfigurationAll(ApplicationID, CompanyID, ParamKey, ApplicationConfigKey, null, null, out ErrorMessage);
                if (dt.Rows.Count <= 0)
                    throw new Exception(string.Format("Please Setting Configuration {0} => {1}", ParamKey, ApplicationConfigKey));
                if (ErrorMessage.Length > 0)
                    throw new Exception(ErrorMessage);

                Query = dt.Rows[0]["ApplicationConfigurationValue2"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Query;
        }

        public static string GetConnectionString(int CompanyId, string pServer)
        {
            string ConStr = string.Empty;
            try
            {
                string ErrorMessage = string.Empty;
                string DBINSTANCE = string.Empty;
                string DBNAME = string.Empty;
                string DBSERVER = string.Empty;
                string USERNAME = string.Empty;
                string PASSWORD = string.Empty;
                DataTable dt = new DataTable("dtConstr");

                dt = CSoapHeader.ClientWs.GetCompanyConfigurationAll(CompanyId, "DBINSTANCE", string.Format("DBINSTANCE{0}", pServer), null, null, out ErrorMessage);
                if (dt.Rows.Count <= 0)
                    throw new Exception(string.Format("Please Setting DBInstance for {0}", pServer));
                if (ErrorMessage.Length > 0)
                    throw new Exception(ErrorMessage);
                DBINSTANCE = dt.Rows[0]["Value1"].ToString();

                dt = CSoapHeader.ClientWs.GetCompanyConfigurationAll(CompanyId, "DBNAME", string.Format("DBNAME{0}", pServer), null, null, out ErrorMessage);
                if (dt.Rows.Count <= 0)
                    throw new Exception(string.Format("Please Setting DBName for {0}", pServer));
                if (ErrorMessage.Length > 0)
                    throw new Exception(ErrorMessage);
                DBNAME = dt.Rows[0]["Value1"].ToString();

                dt = CSoapHeader.ClientWs.GetCompanyConfigurationAll(CompanyId, "DBSERVER", string.Format("DBSERVER{0}", pServer), null, null, out ErrorMessage);
                if (dt.Rows.Count <= 0)
                    throw new Exception(string.Format("Please Setting DBServer for {0}", pServer));
                if (ErrorMessage.Length > 0)
                    throw new Exception(ErrorMessage);
                DBSERVER = dt.Rows[0]["Value1"].ToString();

                dt = CSoapHeader.ClientWs.GetCompanyConfigurationAll(CompanyId, "USERNAME", string.Format("DBSERVER{0}", pServer), null, null, out ErrorMessage);
                if (dt.Rows.Count <= 0)
                    throw new Exception(string.Format("Please Setting USERNAME for {0}", pServer));
                if (ErrorMessage.Length > 0)
                    throw new Exception(ErrorMessage);
                USERNAME = dt.Rows[0]["Value1"].ToString();

                dt = CSoapHeader.ClientWs.GetCompanyConfigurationAll(CompanyId, "PASSWORD", string.Format("DBSERVER{0}", pServer), null, null, out ErrorMessage);
                if (dt.Rows.Count <= 0)
                    throw new Exception(string.Format("Please Setting PASSWORD for {0}", pServer));
                if (ErrorMessage.Length > 0)
                    throw new Exception(ErrorMessage);
                PASSWORD = dt.Rows[0]["Value1"].ToString();
                ConStr = string.Format(@"Data Source={0}\{1};Integrated Security=true;initial catalog={2};user id={3};password={4}", DBSERVER, DBINSTANCE, DBNAME, USERNAME, PASSWORD);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ConStr;
        }

        public static string GetConnectionStringApplication(int AppID, int CompanyId, string pServer)
        {
            string ConStr = string.Empty;
            try
            {
                string ErrorMessage = string.Empty;
                string DBINSTANCE = string.Empty;
                string DBNAME = string.Empty;
                string DBSERVER = string.Empty;
                string USERNAME = string.Empty;
                string PASSWORD = string.Empty;
                DataTable dt;

                dt = CSoapHeader.ClientWs.GetApplicationConfigurationAll(AppID, CompanyId, "DBINSTANCE", string.Format("DBINSTANCE{0}", pServer), null, null, out ErrorMessage);
                if (dt.Rows.Count <= 0)
                    throw new Exception(string.Format("Please Setting DBInstance for {0}", pServer));
                if (ErrorMessage.Length > 0)
                    throw new Exception(ErrorMessage);
                DBINSTANCE = dt.Rows[0]["ApplicationConfigurationValue1"].ToString();

                dt = CSoapHeader.ClientWs.GetApplicationConfigurationAll(AppID, CompanyId, "DBNAME", string.Format("DBNAME{0}", pServer), null, null, out ErrorMessage);
                if (dt.Rows.Count <= 0)
                    throw new Exception(string.Format("Please Setting DBName for {0}", pServer));
                if (ErrorMessage.Length > 0)
                    throw new Exception(ErrorMessage);
                DBNAME = dt.Rows[0]["ApplicationConfigurationValue1"].ToString();

                dt = CSoapHeader.ClientWs.GetApplicationConfigurationAll(AppID, CompanyId, "DBSERVER", string.Format("DBSERVER{0}", pServer), null, null, out ErrorMessage);
                if (dt.Rows.Count <= 0)
                    throw new Exception(string.Format("Please Setting DBServer for {0}", pServer));
                if (ErrorMessage.Length > 0)
                    throw new Exception(ErrorMessage);
                DBSERVER = dt.Rows[0]["ApplicationConfigurationValue1"].ToString();

                dt = CSoapHeader.ClientWs.GetApplicationConfigurationAll(AppID, CompanyId, "USERNAME", string.Format("USERNAME{0}", pServer), null, null, out ErrorMessage);
                if (dt.Rows.Count <= 0)
                    throw new Exception(string.Format("Please Setting USERNAME for {0}", pServer));
                if (ErrorMessage.Length > 0)
                    throw new Exception(ErrorMessage);
                USERNAME = dt.Rows[0]["ApplicationConfigurationValue2"].ToString();

                dt = CSoapHeader.ClientWs.GetApplicationConfigurationAll(AppID, CompanyId, "PASSWORD", string.Format("PASSWORD{0}", pServer), null, null, out ErrorMessage);
                if (dt.Rows.Count <= 0)
                    throw new Exception(string.Format("Please Setting PASSWORD for {0}", pServer));
                if (ErrorMessage.Length > 0)
                    throw new Exception(ErrorMessage);
                PASSWORD = dt.Rows[0]["ApplicationConfigurationValue2"].ToString();
                if (!string.IsNullOrEmpty(DBINSTANCE))
                    ConStr = string.Format(@"Data Source={0}\{1};initial catalog={2};user id={3};password={4};", DBSERVER, DBINSTANCE, DBNAME, USERNAME, PASSWORD);
                else
                    ConStr = string.Format(@"Data Source={0};initial catalog={1};user id={2};password={3};", DBSERVER, DBNAME, USERNAME, PASSWORD);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ConStr;
        }


    }
}

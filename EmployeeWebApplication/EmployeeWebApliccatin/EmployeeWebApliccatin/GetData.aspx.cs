using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeWebApliccatin
{
    public partial class GetData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // q=1 -->get our team list
            if (Request.Params["q"] != null && Request.Params["q"] == "1")
            {



                //do stuff
                List<Employee> empLst = Employee.GetEmployeeList();
                

                //string json = "{\"name\":\"Joe\"}";
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string myEmpLst = javaScriptSerializer.Serialize(empLst);
                string json = empLst.ToString();
                Response.Clear();
                Response.ContentType = "application/json";
                Response.Write(myEmpLst);
                Response.End();

            }
            //q=2 -->get phone list
            else if (Request.Params["q"] != null && Request.Params["q"] == "2")
            {

                //do stuff
                ProductType PType = (ProductType)Convert.ToByte(Request.Params["TP"]);
                List<Phone> phList = Phone.GetPhonesListByType(PType);

                string myJson = "{\"OSs\":[{\"n\":\"Android\", \"val\": \"1\"}, {\"n\":\"IOS\", \"val\":\"2\"}], \"Brds\":[{\"n\":\"Sumsung\" , \"val\":\"1\"},{\"n\":\"Apple\" , \"val\":\"2\"},{\"n\":\"Microsoft\" , \"val\":\"3\"}, {\"n\":\"Sony\" , \"val\":\"4\"}] ,\"DataArr\":[";
                int i = 1;
                foreach (Phone myPh in phList) {
                    myJson += "{\"Id\":" + myPh.Id.ToString() + ",\"Name\":" + "\"" + myPh.Name.ToString() + "\"" + ",\"Price\":" + "\"" + myPh.Price.ToString() + "\""+ ",\"DPrice\":" + "\"" + myPh.DiscountPrice.ToString() + "\"" + ",\"Brand\":";
                    #region Brand Name SC
                    switch (myPh.Brand)
                    {
                        case Brands.Apple:

                            myJson += "\"Apple\"";
                            break;
                        case Brands.Microsoft:
                            myJson += "\"Microsoft\"";
                            break;
                        case Brands.Samsung:
                            myJson += "\"Samsung\"";
                            break;
                        case Brands.Sony:
                            myJson += "\"Sony\"";
                            break;
                        case Brands.HTC:
                            myJson += "\"HTC\"";
                            break;
                    }
                    #endregion

                    myJson += ",\"PublishDate\":" + (myPh.PublishDate == null ? "null" : "\"" + myPh.PublishDate.ToString() + "\"");
                    myJson += ",\"ImgUrl\":" + (myPh.ImgURL == null ? "null" : "\"" + myPh.ImgURL.ToString() + "\"");
                    myJson += ",\"PrType\":";
                    #region Product Type SC
                    switch (myPh.PrType)
                    {
                        case ProductType.Phone:
                            myJson += "\"Phone\"";
                            break;
                        case ProductType.Tablet:
                            myJson += "\"Tablet\"";
                            break;

                    }
                    #endregion

                    myJson += ",\"Available\":" + "\""+ myPh.Available + "\"";
                    myJson += ",\"Sale\":" + "\"" + myPh.Sale + "\"";
                    myJson += ",\"Summery\":" + "\"" + myPh.Summery.ToString() + "\"";
                    myJson += ",\"OS\":";
                    #region Operating Sysyem
                    switch (myPh.OS)
                    {
                        case OsType.Android:
                            myJson += "\"Android\"";
                            break;
                        case OsType.IOS:
                            myJson += "\"IOS\"";
                            break;

                    }
                    #endregion
                    if (i == phList.Count())
                        myJson += "}";
                    else {
                        myJson += "},";
                        i++;
                    }

                }               
                myJson += "]}";
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string myPhoneList = javaScriptSerializer.Serialize(myJson);

                Response.Clear();
                Response.ContentType = "application/json";
                Response.Write(myJson);
                Response.End();

            }
            // q = 1-- > get phone count
            else if(Request.Params["q"] != null && Request.Params["q"] == "3")
            {

                int MyProductsCount = Phone.GetPhoneCount();

                Response.Clear();
                Response.ContentType = "application/json";
                Response.Write(MyProductsCount);
                Response.End();

            }
            // q = 4-- > get phone list by Filter
            else if(Request.Params["q"] != null && Request.Params["q"] == "4")
            {

                if (Request.Params["JD"] != null)

                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    dynamic myObj = serializer.Deserialize<object>(Request.Params["JD"].ToString());
                    string PhoneName = myObj["n"];
                    string myOSTypes = "";
                    string myBrands="";
                    float? myMinPrice = null;
                    float? myMaxPrice = null;
                    ProductType PType= (ProductType)Convert.ToByte(myObj["TP"]);
                    
                  if (myObj["OS"] != null) 
                    myOSTypes = myObj["OS"].ToString().Replace("[", "").Replace("]", "");

                  if (myObj["Br"] != null)
                        myBrands = myObj["Br"].ToString().Replace("[", "").Replace("]", "");

                    if (myObj["minP"] != null)
                        myMinPrice = (float)Convert.ToInt32(myObj["minP"]);

                    if (myObj["maxP"] != null)
                        myMaxPrice = (float)Convert.ToInt32(myObj["maxP"]);

                    List<Phone> phoneFilteredList = Phone.GetPhoneListByFilter(null, myBrands, PType, PhoneName, myOSTypes, myMinPrice, myMaxPrice);


                      string myJson = "[";
                    int i = 1;
                    foreach (Phone myPh in phoneFilteredList)
                    {
                        myJson += "{\"Id\":" + myPh.Id.ToString() + ",\"Name\":" + "\"" + myPh.Name.ToString() + "\""+ ",\"Price\":" + "\"" + myPh.Price.ToString() + "\"" + ",\"DPrice\":" + "\"" + myPh.DiscountPrice.ToString() + "\"" + ",\"Brand\":";
                        #region Brand Name SC
                        switch (myPh.Brand)
                        {
                            case Brands.Apple:

                                myJson += "\"Apple\"";
                                break;
                            case Brands.Microsoft:
                                myJson += "\"Microsoft\"";
                                break;
                            case Brands.Samsung:
                                myJson += "\"Samsung\"";
                                break;
                            case Brands.Sony:
                                myJson += "\"Sony\"";
                                break;
                            case Brands.HTC:
                                myJson += "\"HTC\"";
                                break;
                        }
                        #endregion
                        myJson += ",\"PublishDate\":" + (myPh.PublishDate == null ? "null" : "\"" + myPh.PublishDate.ToString() + "\"");
                        myJson += ",\"ImgUrl\":" + (myPh.ImgURL == null ? "null" : "\"" + myPh.ImgURL.ToString() + "\"");
                        myJson += ",\"Sale\":" + "\"" + myPh.Sale + "\"";
                        myJson += ",\"Summery\":" + "\"" + myPh.Summery.ToString() + "\"";
                        myJson += ",\"PrType\":";

                        #region Product Type SC
                        switch (myPh.PrType)
                        {
                            case ProductType.Phone:
                                myJson += "\"Phone\"";
                                break;
                            case ProductType.Tablet:
                                myJson += "\"Tablet\"";
                                break;

                        }
                        #endregion
                        if (i == phoneFilteredList.Count())
                            myJson += "}";
                        else
                        {
                            myJson += "},";
                            i++;
                        }

                    }
                    myJson += "]";
                    var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    string myPhoneList = javaScriptSerializer.Serialize(myJson);

                    Response.Clear();
                    Response.ContentType = "application/json";
                    Response.Write(myJson);
                    Response.End();

                }


           
            }
            // q = 5-- > get phone Item by ID
            else if (Request.Params["q"] != null && Request.Params["q"] == "5")
            {

                //do stuff
                int phoneID = Convert.ToInt32(Request.Params["id"]);
                Phone PhoneItem = Phone.GetPhoneByID(phoneID);

                string myJson = "";

           
                    myJson += "{\"Id\":" + PhoneItem.Id.ToString() + ",\"Name\":" + "\"" + PhoneItem.Name.ToString() + "\""+ ",\"Price\":" + "\"" + PhoneItem.Price.ToString() + "\"" + ",\"DPrice\":" + "\"" + PhoneItem.DiscountPrice.ToString() + "\"" + ",\"Brand\":";
                    #region Brand Name SC
                    switch (PhoneItem.Brand)
                    {
                        case Brands.Apple:

                            myJson += "\"Apple\"";
                            break;
                        case Brands.Microsoft:
                            myJson += "\"Microsoft\"";
                            break;
                        case Brands.Samsung:
                            myJson += "\"Samsung\"";
                            break;
                        case Brands.Sony:
                            myJson += "\"Sony\"";
                            break;
                        case Brands.HTC:
                            myJson += "\"HTC\"";
                            break;
                    }
                    #endregion
                    myJson += ",\"PublishDate\":" + (PhoneItem.PublishDate == null ? "null" : "\"" + PhoneItem.PublishDate + "\"");
                    myJson += ",\"ImgUrl\":" + (PhoneItem.ImgURL == null ? "null" : "\"" + PhoneItem.ImgURL.ToString() + "\"");
                    myJson += ",\"PrType\":";
                    #region Product Type SC
                    switch (PhoneItem.PrType)
                    {
                        case ProductType.Phone:
                            myJson += "\"Phone\"";
                            break;
                        case ProductType.Tablet:
                            myJson += "\"Tablet\"";
                            break;

                    }


                #endregion

                myJson += ",\"Available\":" + "\"" + PhoneItem.Available + "\"";
                myJson += ",\"Sale\":" + "\"" + PhoneItem.Sale + "\"";
                myJson += ",\"Summery\":" + "\"" + PhoneItem.Summery.ToString() + "\"";
                myJson += ",\"Des\":" + "\"" + PhoneItem.Description.ToString() + "\"";
                myJson += ",\"OS\":";
                #region Operating Sysyem
                switch (PhoneItem.OS)
                {
                    case OsType.Android:
                        myJson += "\"Android\"";
                        break;
                    case OsType.IOS:
                        myJson += "\"IOS\"";
                        break;

                } 
                #endregion
                myJson += "}";
       

                
               // myJson += "]";
                var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string myPhoneItem = javaScriptSerializer.Serialize(myJson);

                Response.Clear();
                Response.ContentType = "application/json";
                Response.Write(myJson);
                Response.End();

            }

        }

    }
}



  


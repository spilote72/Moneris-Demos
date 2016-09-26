namespace Moneris
{
    using System;
    using System.Collections;
    public class TestRiskCheckAttribute
    {
        public static void Main(string[] args)
        {
            string store_id = "moneris";
            string api_token = "hurgle";
            string order_id = "Test" + DateTime.Now.ToString("yyyyMMddhhmmss");
            string service_type = "session";
            string processing_country_code = "CA";
            bool status_check = false;

            AttributeQuery aq = new AttributeQuery();
            aq.SetOrderId(order_id);
            aq.SetServiceType(service_type);
            aq.setDeviceId("");
            aq.setAccountLogin("13195417-8CA0-46cd-960D-14C158E4DBB2");
            aq.setPasswordHash("489c830f10f7c601d30599a0deaf66e64d2aa50a");
            aq.setAccountNumber("3E17A905-AC8A-4c8d-A417-3DADA2A55220");
            aq.setAccountName("4590FCC0-DF4A-44d9-A57B-AF9DE98B84DD");
            aq.setAccountEmail("3CAE72EF-6B69-4a25-93FE-2674735E78E8@test.threatmetrix.com");
            //aq.setCCNumberHash("4242424242424242");
            //aq.setIPAddress("192.168.0.1");
            //aq.setIPForwarded("192.168.1.0");
            aq.setAccountAddressStreet1("3300 Bloor St W");
            aq.setAccountAddressStreet2("4th Flr West Tower");
            aq.setAccountAddressCity("Toronto");
            aq.setAccountAddressState("Ontario");
            aq.setAccountAddressCountry("Canada");
            aq.setAccountAddressZip("M8X2X2");
            aq.setShippingAddressStreet1("3300 Bloor St W");
            aq.setShippingAddressStreet2("4th Flr West Tower");
            aq.setShippingAddressCity("Toronto");
            aq.setShippingAddressState("Ontario");
            aq.setShippingAddressCountry("Canada");
            aq.setShippingAddressZip("M8X2X2");

            HttpsPostRequest mpgReq = new HttpsPostRequest();
            mpgReq.SetProcCountryCode(processing_country_code);
            mpgReq.SetTestMode(true); //false or comment out this line for production transactions
            mpgReq.SetStoreId(store_id);
            mpgReq.SetApiToken(api_token);
            mpgReq.SetTransaction(aq);
            mpgReq.SetStatusCheck(status_check);
            mpgReq.Send();

            try
            {
                Receipt receipt = mpgReq.GetReceipt();

                Hashtable results = new Hashtable();
                string[] rules;

                Console.WriteLine("ResponseCode = " + receipt.GetResponseCode());
                Console.WriteLine("Message = " + receipt.GetMessage());
                Console.WriteLine("TxnNumber = " + receipt.GetTxnNumber());

                results = receipt.GetResult();

                //Iterate through the response
                IDictionaryEnumerator response = results.GetEnumerator();
                while (response.MoveNext())
                {
                    Console.WriteLine(response.Key.ToString() + " = " + response.Value.ToString());
                }


                //Iterate through the rules that were fired
                rules = receipt.GetRules();

                for (int i = 0; i < rules.Length; i++)
                {
                    Console.WriteLine("RuleName = " + rules[i]);
                    Console.WriteLine("RuleCode = " + receipt.GetRuleCode(rules[i]));
                    Console.WriteLine("RuleMessageEn = " + receipt.GetRuleMessageEn(rules[i]));
                    Console.WriteLine("RuleMessageFr = " + receipt.GetRuleMessageFr(rules[i]));
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    } // end TestRiskCheckAttribute
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
[ServiceContract]
public interface IService
{

	[OperationContract]
	bool IsPrime(int num);

    [OperationContract]
    int SumOfDigits(int num);

    [OperationContract]
    string ReverseString(string str);

    [OperationContract]
    string HtmlTags(string tag, string data);

    [OperationContract]
    int[] SortNumbers(int[] nums, bool ascending);

}
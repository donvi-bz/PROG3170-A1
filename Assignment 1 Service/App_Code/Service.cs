using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
	public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

	public bool IsPrime(int num) {
		for (int i = 2; i < num / 2; i++) {
			if (num % i == 0) {
				return false;
			}
		}
		return true;
	}

	public int SumOfDigits(int num) {
		int workNum = num;
		int sum = 0;
		while (workNum > 0) {
			sum += workNum % 10;
			workNum /= 10;
		}
		return sum;
    }

    public string ReverseString(string str) {
		return new string(str.Reverse().ToArray());
    }

    public string HtmlTags(string tag, string data) {
		return "<" + tag + ">" + data + "</" + tag + ">";
    }

	public int[] SortNumbers(int[] nums, bool ascending) {
		Array.Sort(nums);
		if (!ascending) {
			Array.Reverse(nums);
		}
        return nums;
	}
}

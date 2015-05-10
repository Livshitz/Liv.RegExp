using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Liv.RegExp
{
	public static class Regex
	{
		public class RegMatches : Dictionary<string, List<string>> { }
		public static String RegMatchSingle(string aInput, string aPattern)
		{
			string ret = null;
			var res = MatchGroups(aInput, aPattern);
			if (res == null || res.Count == 0) return ret;
			if (res.Keys == null || res.Values.Count == 0) return ret;

			var e = res.GetEnumerator();
			e.MoveNext();
			if (e.Current.Value == null) return ret;

			ret = e.Current.Value[0];

			return ret;
		}

		public class RegMatche2
		{
			public string GroupName { get; set; }
			public string Value { get; set; }
		}

		public static RegMatche2[] Match(string aInput, string aPattern)
		{
			var ret = new List<RegMatche2>();
			var groupNames = new List<string>();

			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(aPattern);
			MatchCollection matchCollection = regex.Matches(aInput);

			if (matchCollection.Count == 0) return null;

			// Get group names
			foreach (string groupName in regex.GetGroupNames())
			{
				if (groupName == "0") continue;
				groupNames.Add(groupName);
			}

			foreach (Match match in matchCollection)
			{
				foreach (var g in groupNames)
				{
					var m = match.Groups[g];
					if (m == null || m.Length == 0) continue;
					ret.Add(new RegMatche2()
					{
						GroupName = g,
						Value = match.Value
					});
				}
			}

			return ret.ToArray();
		}

		public static RegMatches MatchGroups(string aInput, string aPattern)
		{
			RegMatches ret = new RegMatches();

			System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(aPattern);
			MatchCollection matchCollection = regex.Matches(aInput);

			foreach (string groupName in regex.GetGroupNames())
			{
				if (groupName == "0") continue;
				ret.Add(groupName, new List<string>());
			}

			bool isFound = false;
			if (matchCollection.Count != 0)
			{
				foreach (Match match in matchCollection)
				{
					foreach (string groupName in ret.Keys)
					{
						foreach (Capture cap in match.Groups[groupName].Captures)
						{
							string val = cap.Value.Trim();
							if (val == "") continue;
							ret[groupName].Add(cap.Value.Trim());
							if (!isFound) isFound = true;
						}
					}
				}
			}

			if (!isFound) return null;
			return ret;
		}
	}

}

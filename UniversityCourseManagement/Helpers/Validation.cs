namespace UniversityCourseManagement.Helpers
{
	public class Validation
	{
        public bool CheckSpecialChar (string Name)
		{
			string specialChar = @"!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~";
			foreach (var item in specialChar)
			{
				if (Name.Contains(item))
				{
					return true;
				}
			}
			return false;
		}
    }
}

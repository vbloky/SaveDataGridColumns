namespace SaveDataGrid.ViewModels
{
	public class GridItem
	{
		private static int _idSeed = 0;
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Height { get; set; }

		public GridItem(string firstName, string lastName, int height)
		{
			ID = ++_idSeed;
			FirstName = firstName;
			LastName = lastName;
			Height = height;
		}
	}
}

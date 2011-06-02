using Machine.Specifications;

namespace PathMate.Tests
{
	public class a_directory_path_that_is_absolute
	{
		protected static DirectoryPath path;
		
		Establish context = delegate {
			path = new DirectoryPath(@"c:\projects");
		};

		It returns_the_directory_as_a_string = () =>
			path.Path.ShouldEqual(@"c:\projects");

		It can_be_implicitly_converted_to_a_string = delegate {
			string s = path;
			s.ShouldEqual(@"c:\projects");
		};

		It is_equal_to_another_instance_of_the_same_path = () =>
			path.ShouldEqual(new DirectoryPath(@"c:\projects"));

		It can_be_implicitly_created_from_a_string = delegate {
			DirectoryPath newpath = @"c:\projects";
			newpath.ShouldEqual(path);
		};
	}
}
using System;
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
			path.ToString().ShouldEqual(@"c:\projects");

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

		It can_be_combined_with_a_relative_directory_path = () =>
			(path + new DirectoryPath(@"test\path")).ShouldEqual(new DirectoryPath(@"c:\projects\test\path"));

		It can_be_combine_with_multiple_directories_and_a_file_path = () =>
			(path + new DirectoryPath(@"test") + new DirectoryPath("path") + new FilePath("file.txt"))
				.ShouldEqual(new FilePath(@"c:\projects\test\path\file.txt"));

		It can_be_combined_implicitly = () =>
			(path + "test" + "path" + new FilePath("file.txt"))
				.ShouldEqual(new FilePath(@"c:\projects\test\path\file.txt"));

		It should_stay_the_same_when_asked_to_be_made_absolute = () =>
			path.MakeAbsolute().ShouldEqual(path);
	}

	public class a_directory_path_that_is_relative
	{
		protected static DirectoryPath path;

		Establish context = delegate {
			path = new DirectoryPath(@"projects\pathmate");
		};

		It can_be_made_absolute_based_on_the_current_directory = () =>
			path.MakeAbsolute().ShouldEqual(new DirectoryPath(
				System.IO.Path.Combine(Environment.CurrentDirectory, @"projects\pathmate")));
	}
}
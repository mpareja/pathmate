using System;
using Machine.Specifications;

namespace PathMate.Tests
{
	public class a_file_path_that_is_simply_a_filename
	{
		protected static FilePath path;

		Establish context = delegate {
			path = new FilePath("test.txt");
		};

		It returns_the_filename_as_string = () =>
			path.ToString().ShouldEqual("test.txt");

		It can_be_implicitly_converted_to_a_string = delegate {
			string actual = path;
			actual.ShouldEqual(@"test.txt");
		};

		It cannot_be_made_relative_to_a_null_directory_path = () =>
			typeof(ArgumentException).ShouldBeThrownBy(() => path.RelativeTo(null));

		It can_be_made_relative_to_a_directory_path = () =>
			path.RelativeTo(@"c:\projects").ToString().ShouldEqual(@"c:\projects\test.txt");

		It can_be_made_relative_using_plus_operator = () =>
			(new DirectoryPath(@"c:\projects") + path).ShouldEqual(new FilePath(@"c:\projects\test.txt"));

		It is_considered_relative = () =>
			path.IsRelative.ShouldBeTrue();

		It can_return_the_filename_portion = () =>
			path.FileName.ShouldEqual(new FilePath(@"test.txt"));

		It can_return_the_filename_without_extension = () =>
			path.FileNameWithoutExtension.ShouldEqual(new FilePath(@"test"));
	}

	public class a_file_path_that_is_relative_and_multiple_directories_deep
	{
		protected static FilePath path;
		
		Establish context = delegate {
			path = new FilePath(@"test1\test2\test.txt");
		};

		It is_considered_relative = () =>
			path.IsRelative.ShouldBeTrue();

		It can_be_made_relative_to_a_directory_path = () =>
			path.RelativeTo(@"c:\projects").ToString().ShouldEqual(@"c:\projects\test1\test2\test.txt");

		It can_be_made_absolute_based_on_the_current_directory = () =>
			path.MakeAbsolute().ShouldEqual(new FilePath(System.IO.Path.Combine(Environment.CurrentDirectory, path)));
	}

	public class a_file_path_that_is_absolute
	{
		protected static FilePath path;

		Establish context = delegate
		{
			path = new FilePath(@"c:\test\test.txt");
		};

		It is_not_considered_relative = () =>
			path.IsRelative.ShouldBeFalse();

		It cannot_be_made_relative_to_a_directory_path = () =>
			typeof(InvalidOperationException).ShouldBeThrownBy(() => path.RelativeTo(@"c:\projects"));

		It can_return_the_filename_portion = () =>
			path.FileName.ShouldEqual(new FilePath(@"test.txt"));

		It can_return_the_filename_without_extension = () =>
			path.FileNameWithoutExtension.ShouldEqual(new FilePath(@"test"));

		It should_stay_the_same_when_asked_to_be_made_absolute = () =>
			path.MakeAbsolute().ShouldEqual(path);
	}

	public class a_file_path_with_multiple_periods_in_the_filename
	{
		protected static FilePath path;

		Establish context = delegate
		{
			path = new FilePath(@"c:\test\test.part.1.txt");
		};

		It should_return_all_parts_of_the_filename_except_the_last = () =>
			path.FileNameWithoutExtension.ShouldEqual(new FilePath(@"test.part.1"));
	}

}
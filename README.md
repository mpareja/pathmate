Guiding principles

* be string friendly - don't force everyone to do new FilePath(@"file.txt")
* be strongly typed - don't allow combining 2 non-directory paths
* be immutable
* be explicit about file-system access (until we sort out a testing story)
* be interoperable - support alternate file separators, case-sensitivity
* be a pleasure to work with - plenty of helpers to get the job done

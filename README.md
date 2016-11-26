ndesk-options
=============

This is ndesk-options, a program option parser for .NET Core.

It is based on Jonathan Pryor's NDesk.Options library with minor modifications
that allow compilation against the .NET Standard 1.0 API.

The URL of the original project is: http://www.ndesk.org/Options

Overview:
--------

It takes advantage of C# 3.0 features such as collection initializers and
lambda delegates to provide a short, concise specification of the option 
names to parse, whether or not those options support values, and what to do 
when the option is encountered.  It's entirely callback based:

	var verbose = 0;
	var show_help = false;
	var names = new List<string> ();

	var p = new OptionSet () {
		{ "v|verbose", v => { if (v != null) ++verbose; } },
		{ "h|?|help",  v => { show_help = v != null; } },
		{ "n|name=",   v => { names.Add (v); } },
	};

Distribution:
------------

All .NET Standard libraries are distributed and consumed as NuGet packages.
To create a NuGet package of ndesk-options, simply use the `dotnet pack`
command.

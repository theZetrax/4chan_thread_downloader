#!/usr/bin/perl
use strict; # I guess strict type checking ?? --_(-_-)_--
use warnings; # show warnings
use 5.30.0; # Using Perl v5.30.0
use Getopt::Long qw(GetOptions);

## Making a class, inherits from Html::Parser super class
## Implements the start method, or subroutine.
package HtmlParser;
use base qw(HTML::Parser);

# Looks for <img> tags and extracts their src attribute
sub start {
    my ($self, $tagname, $attr, $attrseq, $origtext) = @_;

    if ($tagname eq 'img') {
        print "Image found: ", $attr->{ src }, "\n";
    }
}

package main;

# Usage display, checking if both 'io' and 'html' flag are not used
if ( !(grep { 
        $_ eq '--io' ||
        $_ eq '--html' ||
        $_ eq '--file' 
    } @ARGV) ) 
{
    say "Usage: $0 --io --html <HTML STRING>";
    say "\tEither --io , --file or --html must be used";
    say "\t--io\n\t\tBoolean, either true or false, default false.";
    say "\t--html\n\t\tHtml string for parsing.";
    say "\t--file\n\t\tFile path with html data for parsing.";
    exit;
} elsif ( grep { $_ eq '--io' } @ARGV ) {
    # FIXME: Automate it to check for if any is being used with other
    if ( grep { $_ eq '--file'} @ARGV ) {
        say "Usage: $0 --io --html <HTML STRING>";
        say "\tCan't use both --file and --io at the same time, either one must be used.";
        say "\t--io\n\t\tBoolean, either true or false, default false.";
        say "\t--html\n\t\tHtml string for parsing.";
        say "\t--file\n\t\tFile path with html data for parsing.";
        exit;
    }
}

# Console argument variables
my $useIO;
my $filePath;
my $console_html;

# Getting argument options, storing them in console argument variables.
GetOptions('io' => \$useIO, 'html=s' => \$console_html, 'file=s' => \$filePath ) or die "Usage: $0 --html <HTML STRING> or --file <FILE PATH>\n";



# processing for using IO or Console argument
if($useIO) {
    my $html_string = <>;
    say "Welcome";

    $html_string = <<EOHTML;
    <html>
        <head>
            <title>Boi</title>
        </head>
        <body>
            <img src="https://developer.mozilla.org/static/img/favicon144.png" alt="Visit the MDN site">
            <img src="https://www.google.com/google-logo.png" alt="Google Logo Here">
            <img src="https://developer.mozilla.org/static/img/favicon144.png" alt="Visit the MDN site">
        </body>
    </html>
EOHTML

    my $parser = HtmlParser->new;
    $parser->parse( $html_string );
} else {
    my $html_string = $console_html;
    print $html_string;
}

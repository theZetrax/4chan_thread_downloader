#!/usr/bin/perl
use strict; # I guess strict type checking ?? --_(-_-)_--
use warnings; # show warnings
use 5.30.0; # Using Perl v5.30.0
use Getopt::Long qw(GetOptions);

## Making a class, inherits from Html::Parser super class
## Implements the start method, or subroutine.
package HtmlParser;
use base qw(HTML::Parser);

# Constructor
sub new {
    my ($class) = @_;
    my $self = $class->SUPER::new();

    my @links = ();
    $self->{_links} = \@links;

    bless $self, $class;
    return $self;
}

# returns links joined together with new line character.
sub getLinks {
    my ($self) = @_;
    my $links = $self->{_links}; # Getting Links array reference
    return join("\n", @$links);
}

# Looks for <img> tags and extracts their src attribute
sub start {
    my ($self, $tagname, $attr, $attrseq, $origtext) = @_;
    my $links = $self->{_links};

    # Get tag attribute if 
    if ($tagname eq 'img') {
        my $href = $attr->{ src }; # Reading src attribute
        $href =~ s/\/\///ig; # Remove '\\' characters 
        push(@$links, $href);
    }
}

# Main Program Section
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

# Initiating Parser
my $parser = new HtmlParser();

# processing for using IO or Console argument
if($useIO) {
    my $html_string = <>; # Reading input from input stream
    $parser->parse( $html_string ); # Parsing HTML Data from io
    print $parser->getLinks(); # Printing links
    exit;
}

# Read html data from file.
if ($filePath) {
    $parser->parse_file($filePath); # Reading & Parsing Html from file.
    print $parser->getLinks(); # printing links
    exit;
}

# Read Console Input
if ($console_html) {
    $parser->parse( $console_html ); # Parsing html data from console argumnet input
    print $parser->getLinks(); # Printing links
    exit;
}

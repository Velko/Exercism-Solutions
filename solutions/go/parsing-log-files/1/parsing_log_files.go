package parsinglogfiles

import (
    "regexp"
    "fmt"
)

func IsValidLine(text string) bool {
	re := regexp.MustCompile(`^\[([A-Z]{3})\]`)
    s := re.FindStringSubmatch(text)

    return len(s) == 2 && (
        s[1] == "TRC" ||
        s[1] == "DBG" ||
        s[1] == "INF" ||
        s[1] == "WRN" ||
        s[1] == "ERR" ||
        s[1] == "FTL")
}

func SplitLogLine(text string) []string {
	re := regexp.MustCompile(`<[~\*=\-]*>`)
    return re.Split(text, -1)
}

func CountQuotedPasswords(lines []string) int {
    re := regexp.MustCompile(`(?i)".*password.*"`)
	count := 0
    for _, line := range lines {
        if re.MatchString(line) {
            count += 1
        }
    }

    return count
}

func RemoveEndOfLineText(text string) string {
    re := regexp.MustCompile(`end-of-line\d+`)
    return re.ReplaceAllString(text, "")
}

func TagWithUserName(lines []string) []string {
	re := regexp.MustCompile(`User\s+(\S+)`)
    for i, line := range lines {
        s := re.FindStringSubmatch(line)
        if len(s) == 2 {
            lines[i] = fmt.Sprintf("[USR] %s %s", s[1], line)
        }
    }

    return lines
}

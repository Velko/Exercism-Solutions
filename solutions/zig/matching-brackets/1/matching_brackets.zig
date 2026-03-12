const std = @import("std");
const mem = std.mem;

pub fn isBalanced(allocator: mem.Allocator, s: []const u8) !bool {
    var expectedClosing = std.ArrayList(u8).init(allocator);
    defer expectedClosing.deinit();

    for (s) |c| {
        switch (c) {
            '(' => {
                try expectedClosing.append(')');
            },
            '{' => {
                try expectedClosing.append('}');
            },
            '[' => {
                try expectedClosing.append(']');
            },
            ')', '}', ']' => {
                if (expectedClosing.popOrNull() != c)
                    return false;
            },
            else => {}
        }
    }

    return expectedClosing.items.len == 0;
}

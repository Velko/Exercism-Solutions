#ifndef CIRCULAR_BUFFER_H
#define CIRCULAR_BUFFER_H

#include <stddef.h>
#include <stdint.h>
#include <stdbool.h>

typedef int buffer_value_t;

struct _circular_buffer
{
    size_t capacity;
    size_t read_index;
    size_t write_index;
    buffer_value_t data[];
};

typedef struct _circular_buffer circular_buffer_t;

circular_buffer_t *new_circular_buffer(size_t capacity);
void delete_buffer(circular_buffer_t *buffer);

int16_t write(circular_buffer_t *buffer, buffer_value_t value);
int16_t overwrite(circular_buffer_t *buffer, buffer_value_t value);
int16_t read(circular_buffer_t *buffer, buffer_value_t *value);
void clear_buffer(circular_buffer_t *buffer);

bool is_empty(circular_buffer_t *buffer);
bool is_full(circular_buffer_t *buffer);

#endif

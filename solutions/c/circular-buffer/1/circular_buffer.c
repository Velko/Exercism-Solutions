#include "circular_buffer.h"
#include <stdlib.h>
#include <errno.h>

static size_t wrapped_read_index(circular_buffer_t *buffer);
static size_t wrapped_write_index(circular_buffer_t *buffer);

circular_buffer_t *new_circular_buffer(size_t capacity)
{
    circular_buffer_t *buffer = malloc(
        sizeof(circular_buffer_t)
        + sizeof(buffer_value_t) * capacity
        );

    buffer->capacity = capacity;
    buffer->read_index = 0;
    buffer->write_index = 0;

    return buffer;
}

void delete_buffer(circular_buffer_t *buffer)
{
    free(buffer);
}

int16_t write(circular_buffer_t *buffer, buffer_value_t value)
{
    if (is_full(buffer))
    {
        errno = ENOBUFS;
        return EXIT_FAILURE;
    }
    
    buffer->data[wrapped_write_index(buffer)] = value;
    ++buffer->write_index;

    return EXIT_SUCCESS;
}

int16_t overwrite(circular_buffer_t *buffer, buffer_value_t value)
{
    if (is_full(buffer))
        ++buffer->read_index;
    
    buffer->data[wrapped_write_index(buffer)] = value;
    ++buffer->write_index;

    return EXIT_SUCCESS;
}

int16_t read(circular_buffer_t *buffer, buffer_value_t *value)
{
    if (is_empty(buffer))
    {
        errno = ENODATA;
        return EXIT_FAILURE;
    }

    *value = buffer->data[wrapped_read_index(buffer)];
    ++buffer->read_index;

    return EXIT_SUCCESS;
}

void clear_buffer(circular_buffer_t *buffer)
{
    buffer->read_index = 0;
    buffer->write_index = 0;
}

bool is_empty(circular_buffer_t *buffer)
{
    return buffer->write_index == buffer->read_index;
}

bool is_full(circular_buffer_t *buffer)
{
    return buffer->write_index - buffer->read_index == buffer->capacity;
}

static size_t wrapped_write_index(circular_buffer_t *buffer)
{
    return buffer->write_index % buffer->capacity;   
}

static size_t wrapped_read_index(circular_buffer_t *buffer)
{
    return buffer->read_index % buffer->capacity;   
}
#include "custom_multithreaded_object.h"

custom_multithreaded_object::custom_multithreaded_object()
{
}

custom_multithreaded_object::~custom_multithreaded_object()
{
}

void custom_multithreaded_object::register_thread_(void_action_mediator* mediator)
{
    this->register_thread(*mediator, &void_action_mediator::on_action_handler);
}
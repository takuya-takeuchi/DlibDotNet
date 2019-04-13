#ifndef _CPP_LOGGER_LOGGER_H_
#define _CPP_LOGGER_LOGGER_H_

#include "../export.h"
#include <dlib/logger.h>
#include "../shared.h"

DLLEXPORT dlib::logger* logger_new(const char* name)
{
    return new dlib::logger(std::string(name));
}

DLLEXPORT void logger_delete(dlib::logger* logger)
{
    delete logger;
}

DLLEXPORT void logger_set_level(dlib::logger* logger, log_level log_level)
{

    switch(log_level)
    {
        case log_level::All:
            logger->set_level(dlib::LALL);
            break;
        case log_level::None:
            logger->set_level(dlib::LNONE);
            break;
        case log_level::Trace:
            logger->set_level(dlib::LTRACE);
            break;
        case log_level::Debug:
            logger->set_level(dlib::LDEBUG);
            break;
        case log_level::Info:
            logger->set_level(dlib::LINFO);
            break;
        case log_level::Warn:
            logger->set_level(dlib::LWARN);
            break;
        case log_level::Error:
            logger->set_level(dlib::LERROR);
            break;
        case log_level::Fatal:
            logger->set_level(dlib::LFATAL);
            break;
    }
}

DLLEXPORT void logger_operator_left_shift(dlib::logger* logger, log_level log_level, const char* message)
{
    auto& l = *logger;

    switch(log_level)
    {
        case log_level::All:
            l << dlib::LALL << std::string(message);
            break;
        case log_level::None:
            l << dlib::LNONE << std::string(message);
            break;
        case log_level::Trace:
            l << dlib::LTRACE << std::string(message);
            break;
        case log_level::Debug:
            l << dlib::LDEBUG << std::string(message);
            break;
        case log_level::Info:
            l << dlib::LINFO << std::string(message);
            break;
        case log_level::Warn:
            l << dlib::LWARN << std::string(message);
            break;
        case log_level::Error:
            l << dlib::LERROR << std::string(message);
            break;
        case log_level::Fatal:
            l << dlib::LFATAL << std::string(message);
            break;
    }
}

#endif
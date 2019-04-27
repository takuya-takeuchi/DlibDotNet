#ifndef _CPP_LOGGER_CUSTOM_LOGGER_H_
#define _CPP_LOGGER_CUSTOM_LOGGER_H_

#include "../export.h"
#include <dlib/logger/logger_kernel_1.h>
#include "../shared.h"

class custom_logger
{
public:
    custom_logger(void (*log_func)(const std::string*,
                                   const log_level,
                                   const std::string*,
                                   const uint64_t,
                                   const std::string*)):
        m_log_func(log_func)
    {
    }

    ~custom_logger()
    {
        this->m_log_func = nullptr;
    }

    void log(const std::string& logger_name,
             const dlib::log_level& ll,
             const uint64_t thread_id,
             const char* message_to_log)
    {        
        if (this->m_log_func)
        {
            auto plogger_name = new std::string(logger_name);
            auto pmessage_to_log = new std::string(message_to_log);
            std::string* level_name = nullptr;
            log_level l = log_level::All;

            // const log_level LALL  (std::numeric_limits<int>::min(),"ALL");
            // const log_level LNONE (std::numeric_limits<int>::max(),"NONE");
            // const log_level LTRACE(-100,"TRACE");
            // const log_level LDEBUG(0  ,"DEBUG");
            // const log_level LINFO (100,"INFO ");
            // const log_level LWARN (200,"WARN ");
            // const log_level LERROR(300,"ERROR");
            // const log_level LFATAL(400,"FATAL");

            switch(ll.priority)
            {
                case -100:
                    level_name = new std::string("TRACE");
                    l = log_level::Trace;
                    break;
                case 0:
                    level_name = new std::string("DEBUG");
                    l = log_level::Debug;
                    break;
                case 100:
                    level_name = new std::string("INFO ");
                    l = log_level::Info;
                    break;
                case 200:
                    level_name = new std::string("WARN ");
                    l = log_level::Warn;
                    break;
                case 300:
                    level_name = new std::string("ERROR");
                    l = log_level::Error;
                    break;
                case 400:
                    level_name = new std::string("FATAL");
                    l = log_level::Fatal;
                    break;
                default:
                    if (ll.priority == std::numeric_limits<int>::min())
                    {
                        level_name = new std::string("ALL");
                        l = log_level::All;
                    }
                    else if  (ll.priority == std::numeric_limits<int>::max())
                    {
                        level_name = new std::string("NONE");
                        l = log_level::None;
                    }
                    break;
            }

            if (level_name)
                this->m_log_func(plogger_name, l, level_name, thread_id, pmessage_to_log);

            delete level_name;
            delete plogger_name;
            delete pmessage_to_log;
        }
    }

private:
    void (*m_log_func)(const std::string*,
                       const log_level,
                       const std::string*,
                       const uint64_t,
                       const std::string*);
};

DLLEXPORT custom_logger* custom_logger_new(void (*log_func)(const std::string*,
                                                            const log_level,
                                                            const std::string*,
                                                            const uint64_t,
                                                            const std::string*))
{
    return new custom_logger(log_func);
}

DLLEXPORT void custom_logger_delete(custom_logger* logger)
{
    delete logger;
}

DLLEXPORT void custom_set_all_logging_output_hooks(custom_logger* logger)
{
    auto& l = *logger;
    dlib::set_all_logging_output_hooks(l);
}

#endif
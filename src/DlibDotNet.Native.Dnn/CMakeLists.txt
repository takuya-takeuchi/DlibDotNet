cmake_minimum_required(VERSION 3.0.0)
set(PROJ_NAME DlibDotNet.Native.Dnn)

project(${PROJ_NAME} VERSION 0.0.0)

# OS info
message("")
message("-- CMAKE_SYSTEM_INFO_FILE: ${CMAKE_SYSTEM_INFO_FILE}")
message("-- CMAKE_SYSTEM_NAME:      ${CMAKE_SYSTEM_NAME}")
message("-- CMAKE_SYSTEM_PROCESSOR: ${CMAKE_SYSTEM_PROCESSOR}")
message("-- CMAKE_SYSTEM:           ${CMAKE_SYSTEM}")
string (REGEX MATCH "\\.el[1-9]" os_version_suffix ${CMAKE_SYSTEM})
message("-- os_version_suffix:      ${os_version_suffix}")
message("")

# Version info
set(VERSION_MAJOR 19)
set(VERSION_MINOR 15)
set(VERSION_PATCH 0)
set(VERSION_DATE 20181008)

# Only GCC requires -fPIC
if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "GNU")
	set(CMAKE_C_FLAGS "-fPIC")
	set(CMAKE_CXX_FLAGS "-fPIC")
endif()

if (CMAKE_GENERATOR MATCHES "Visual Studio")
	# The file contains a character that cannot be represented in the current code page
	set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /wd4819")
endif()

# Select the release build type by default
if (NOT CMAKE_BUILD_TYPE)
    set(CMAKE_BUILD_TYPE "Release")
endif()


# dlib project submodule 
set(DLIB_IN_PROJECT_BUILD False)
add_subdirectory(${PROJECT_SOURCE_DIR}/../../src/dlib/dlib ${CMAKE_CURRENT_BINARY_DIR}/dlib_build)


FILE(GLOB_RECURSE HEADERS ${PROJECT_SOURCE_DIR}/dlib/*.h
                          ${PROJECT_SOURCE_DIR}/../DlibDotNet.Native/dlib/cuda/cuda.h)
FILE(GLOB_RECURSE SOURCES ${PROJECT_SOURCE_DIR}/dlib/*.cpp
                          ${PROJECT_SOURCE_DIR}/../DlibDotNet.Native/dlib/cuda/cuda.cpp)
FILE(GLOB_RECURSE DLIBDOTNET_SHARED_HEADER1 ${PROJECT_SOURCE_DIR}/../DlibDotNet.Native/dlib/shared.h)
FILE(GLOB_RECURSE DLIBDOTNET_SHARED_HEADER2 ${PROJECT_SOURCE_DIR}/../DlibDotNet.Native/dlib/export.h)

if (${DLIB_USE_CUDA})
configure_file(
    ${CMAKE_CURRENT_SOURCE_DIR}/version-cuda.rc.in
    ${CMAKE_CURRENT_BINARY_DIR}/version-cuda.rc
    @ONLY)

    # include CUDA Runtime Library
    if (MSVC OR MSYS OR MINGW)
        include_directories("$ENV{CUDA_PATH}/include")
        link_libraries("$ENV{CUDA_PATH}/lib/x64/cudart_static.lib")
    elseif(APPLE)
        include_directories("$ENV{CUDA_PATH}/include")
        link_libraries("$ENV{CUDA_PATH}/lib/libcudart_static.a")
    elseif(UNIX AND NOT APPLE)
        include_directories("$ENV{CUDA_PATH}/include")
        link_libraries("$ENV{CUDA_PATH}/lib64/libcudart_static.a")
    else()
        message(FATAL_ERROR "Failed to link CUDA Runtime Library")
    endif()

    add_library(${PROJ_NAME} SHARED ${HEADERS}
                                    ${SOURCES}
                                    ${DLIBDOTNET_SHARED_HEADER1}
                                    ${DLIBDOTNET_SHARED_HEADER2}
                                    ${CMAKE_CURRENT_BINARY_DIR}/version-cuda.rc)
else()
    configure_file(
      ${CMAKE_CURRENT_SOURCE_DIR}/version.rc.in
      ${CMAKE_CURRENT_BINARY_DIR}/version.rc
      @ONLY)

    add_library(${PROJ_NAME} SHARED ${HEADERS}
                                    ${SOURCES}
                                    ${DLIBDOTNET_SHARED_HEADER1}
                                    ${DLIBDOTNET_SHARED_HEADER2}
                                    ${CMAKE_CURRENT_BINARY_DIR}/version.rc)
endif()

target_link_libraries(${PROJ_NAME} dlib::dlib )

set(CompilerFlags
    CMAKE_CXX_FLAGS
    CMAKE_CXX_FLAGS_DEBUG
    CMAKE_CXX_FLAGS_RELEASE
    CMAKE_C_FLAGS
    CMAKE_C_FLAGS_DEBUG
    CMAKE_C_FLAGS_RELEASE
)

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)
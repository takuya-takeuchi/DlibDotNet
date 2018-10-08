cmake_minimum_required(VERSION 3.0.0)
set(PROJ_NAME DlibDotNet.Native)

project(${PROJ_NAME} VERSION 0.0.0)

# Version info
set(VERSION_MAJOR 19)
set(VERSION_MINOR 15)
set(VERSION_PATCH 0)
set(VERSION_DATE 20181008)

# Only GCC requires -fPIC
if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "GNU")
	set(CMAKE_CXX_FLAGS "-fPIC")
endif()

if (CMAKE_GENERATOR MATCHES "Visual Studio")
	# C4819: The file contains a character that cannot be represented in the current code page
	# C4503: identifier' : decorated name length exceeded, name was truncated
	set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /wd4819 /wd4503")
endif()

# Select the release build type by default
if (NOT CMAKE_BUILD_TYPE)
    set(CMAKE_BUILD_TYPE "Release")
endif()

add_subdirectory(${DLIB_PATH}/dlib dlib_build)

FILE(GLOB_RECURSE HEADERS ${PROJECT_SOURCE_DIR}/dlib/*.h)
FILE(GLOB_RECURSE SOURCES ${PROJECT_SOURCE_DIR}/dlib/*.cpp)

if (${DLIB_USE_CUDA})
    configure_file(
        ${CMAKE_CURRENT_SOURCE_DIR}/version-cuda.rc.in
        ${CMAKE_CURRENT_BINARY_DIR}/version-cuda.rc
        @ONLY)

    # include CUDA Runtime Library
    include_directories("$ENV{CUDA_PATH}/include")
    link_libraries("$ENV{CUDA_PATH}/lib/x64/cudart_static.lib")

    add_library(${PROJ_NAME} SHARED ${HEADERS}
                                    ${SOURCES}
                                    ${CMAKE_CURRENT_BINARY_DIR}/version-cuda.rc)
else()
    configure_file(
        ${CMAKE_CURRENT_SOURCE_DIR}/version.rc.in
        ${CMAKE_CURRENT_BINARY_DIR}/version.rc
        @ONLY)

    add_library(${PROJ_NAME} SHARED ${HEADERS}
                                    ${SOURCES}
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
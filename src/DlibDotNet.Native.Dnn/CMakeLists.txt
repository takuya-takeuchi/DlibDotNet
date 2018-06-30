cmake_minimum_required(VERSION 3.0.0)
set(PROJ_NAME DlibDotNet.Native.Dnn)

project(${PROJ_NAME} VERSION 0.0.0)

# Only GCC requires -fPIC
if ("${CMAKE_CXX_COMPILER_ID}" STREQUAL "GNU")
	set(CMAKE_CXX_FLAGS "-fPIC")
endif()

# Select the release build type by default
if (NOT CMAKE_BUILD_TYPE)
    set(CMAKE_BUILD_TYPE "Release")
endif()

add_subdirectory(${DLIB_PATH}/dlib dlib_build)

FILE(GLOB_RECURSE HEADERS ${PROJECT_SOURCE_DIR}/dlib/*.h)
FILE(GLOB_RECURSE SOURCES ${PROJECT_SOURCE_DIR}/dlib/*.cpp)
FILE(GLOB_RECURSE DLIBDOTNET_SHARED_HEADER1 ${PROJECT_SOURCE_DIR}/../DlibDotNet.Native.Dnn/dlib/shared.h)
FILE(GLOB_RECURSE DLIBDOTNET_SHARED_HEADER2 ${PROJECT_SOURCE_DIR}/../DlibDotNet.Native.Dnn/dlib/export.h)
add_library(${PROJ_NAME} SHARED ${HEADERS} ${SOURCES} ${DLIBDOTNET_SHARED_HEADER1} ${DLIBDOTNET_SHARED_HEADER2})

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
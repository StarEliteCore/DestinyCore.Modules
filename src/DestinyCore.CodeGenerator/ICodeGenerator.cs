﻿namespace DestinyCore.CodeGenerator
{
    /// <summary>
    /// 生成器
    /// </summary>
    public interface ICodeGenerator
    {
        /// <summary>
        /// 创建代码文件
        /// </summary>
        /// <param name="projectMetadata"></param>
        void GenerateCode(ProjectMetadata projectMetadata);

        /// <summary>
        /// 创建实体代码
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        CodeData GenerateEntityCode(ProjectMetadata metadata);

        /// <summary>
        /// 生成实体配置代码
        /// </summary>
        /// <param name="metadata">元数据</param>
        /// <returns></returns>
        CodeData GenerateEntityConfigurationCode(ProjectMetadata metadata);


        /// <summary>
        /// 创建输入Dto码
        /// </summary>
        /// <param name="metadata">元数据</param>
        /// <returns></returns>
        CodeData GenerateInputDtoCode(ProjectMetadata metadata);

        /// <summary>
        /// 创建输出代码
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        CodeData GenerateOutputDtoCode(ProjectMetadata metadata);


        /// <summary>
        /// 创建分页Dto代码
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        CodeData GeneratePageDtoCode(ProjectMetadata metadata);

        /// <summary>
        /// 生成控制器
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        CodeData GenerateController(ProjectMetadata metadata);
    }
}

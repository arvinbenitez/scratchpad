using AguaDeMaria.Model;
using AguaDeMaria.Models.Expense;
using AutoMapper;

namespace AguaDeMaria.Configuration.Mappers
{
    public class ExpenseMapperConfiguration
    {
        public static void ConfigureExpense()
        {
            MapExpenseToExpenseDto();
            MapExpenseDtoToExpense();
        }

        private static void MapExpenseToExpenseDto()
        {
            Mapper.CreateMap<Expense, ExpenseDto>()
                .ForMember(x => x.ExpenseCategoryTypeId, o => o.MapFrom(s => s.ExpenseType.ExpenseCategoryId))
                .ForMember(x => x.ExpenseCategory, o => o.MapFrom(s => s.ExpenseType.ExpenseCategory.Name))
                .ForMember(x => x.ExpenseType, o => o.MapFrom(s => s.ExpenseType.Name));
        }

        private static void MapExpenseDtoToExpense()
        {
            Mapper.CreateMap<ExpenseDto, Expense>();
        }
    }
}
import { registerRestaurant } from '@api/register-restaurant'
import { Button } from '@components/ui/button'
import { Checkbox } from '@components/ui/checkbox'
import { Input } from '@components/ui/input'
import { Label } from '@components/ui/label'
import { zodResolver } from '@hookform/resolvers/zod'
import { useMutation } from '@tanstack/react-query'
import { Helmet } from 'react-helmet-async'
import { Controller, useForm } from 'react-hook-form'
import { Link, useNavigate } from 'react-router-dom'
import { toast } from 'sonner'
import { z } from 'zod'

const signUpFormSchema = z.object({
  restaurantName: z.string(),
  managerName: z.string(),
  phone: z
    .string()
    .regex(/\d/g, {
      message: 'Digite somente números',
    })
    .min(10, {
      message: 'Celular inválido',
    })
    .max(11, {
      message: 'Celular inválido',
    }),
  email: z.string().email().optional(),
  termsAccepted: z.boolean(),
})

type SignUpForm = z.infer<typeof signUpFormSchema>

export function SignUp() {
  const {
    register,
    handleSubmit,
    formState: { isSubmitting },
    control,
    watch,
  } = useForm<SignUpForm>({
    resolver: zodResolver(signUpFormSchema),
    defaultValues: {
      termsAccepted: false,
    },
  })
  const navigate = useNavigate()

  const { mutateAsync: registerRestaurantFn } = useMutation({
    mutationFn: registerRestaurant,
  })

  const terms = watch('termsAccepted')

  async function handleSignUp(data: SignUpForm) {
    try {
      await registerRestaurantFn({
        restaurantName: data.restaurantName,
        email: data.email,
        managerName: data.managerName,
        phone: String(data.phone),
      })

      toast.success('Restaurante cadastrado com sucesso!', {
        action: {
          label: 'Login',
          onClick: () => navigate(`/sign-in?email=${data.email}`),
        },
      })
    } catch {
      toast.error('Erro ao cadastrar restaurante.')
    }
  }

  return (
    <>
      <Helmet title="Cadastro" />
      <div className="w-full px-4 py-8 md:w-auto md:p-8">
        <div className="absolute right-4 top-[100%] pb-8 md:top-8">
          <Button variant="outline" asChild className="w-[160px]">
            <Link to="/sign-in">Fazer login</Link>
          </Button>
        </div>

        <div className="flex w-full flex-col justify-center gap-6 md:w-[350px]">
          <div className="flex flex-col gap-2 text-center">
            <h1 className="text-2xl font-semibold tracking-tight">
              Criar conta grátis
            </h1>
            <p className="text-sm text-muted-foreground">
              Seja um parceiro e comece suas vendas!
            </p>
          </div>

          <form className="space-y-4" onSubmit={handleSubmit(handleSignUp)}>
            <div className="space-y-2">
              <Label htmlFor="restaurantName">Nome do estabelecimento</Label>
              <Input
                id="restaurantName"
                type="text"
                {...register('restaurantName')}
              />
            </div>

            <div className="space-y-2">
              <Label htmlFor="managerName">Seu nome</Label>
              <Input
                id="managerName"
                type="text"
                {...register('managerName')}
              />
            </div>

            <div className="space-y-2">
              <Label htmlFor="email">Seu e-mail</Label>
              <Input id="email" type="email" {...register('email')} />
            </div>

            <div className="space-y-2">
              <Label htmlFor="phone">Seu celular</Label>
              <Input
                id="phone"
                type="phone"
                placeholder="(99) 9 9999-9999"
                {...register('phone')}
              />
            </div>

            <div className="flex space-x-2">
              <Controller
                control={control}
                name="termsAccepted"
                render={({ field }) => (
                  <Checkbox
                    ref={field.ref}
                    id="termsAccepted"
                    onCheckedChange={field.onChange}
                    checked={field.value}
                  />
                )}
              />
              <Label
                htmlFor="termsAccepted"
                className="cursor-pointer text-xs font-normal leading-tight text-muted-foreground"
              >
                Concordo com nossos termos de serviço e políticas de
                privacidade.
              </Label>
            </div>

            <Button
              className="w-full"
              type="submit"
              disabled={isSubmitting || !terms}
            >
              Finalizar cadastro
            </Button>
          </form>
        </div>
      </div>
    </>
  )
}
